# Dynatrace-ECO Bidirectional Connectivity Architecture

## Problem Statement

**Current Working Flow:**
```
ECO SIT (AWS) → ActiveGateway → non-prod AVS Dynatrace EdgeConnect → Dynatrace SIT (GCP GKE)
```

**Problem:**
When Dynatrace SIT needs to make calls back to ECO SIT (AWS), ECO SIT requires IP whitelisting but can only whitelist **one IP address**. However, Dynatrace SIT running on GKE has multiple possible outbound IPs.

## Solution Architecture

### Complete Bidirectional Flow Diagram

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                        NETWORK FLOW DIAGRAM                                  │
├─────────────────────────────────────────────────────────────────────────────┤

┌─────────────┐    ┌──────────────┐    ┌─────────────┐    ┌──────────────┐
│   ECO SIT   │◄──►│ActiveGateway │◄──►│ EdgeConnect │◄──►│ Dynatrace SIT│
│ (  AWS  )   │    │   (Telus)    │    │ (non-prod   │    │   (GCP GKE)  │
│             │    │              │    │    AVS)     │    │              │
└─────────────┘    └──────────────┘    └─────────────┘    └──────────────┘
       ▲                                                           │
       │                                                           │
       │           ┌─────────────────────────────────────────────┘
       │           │
       │           ▼
       │    ┌──────────────┐
       └────│ Istio Egress │ (Single Static IP)
            │   Gateway    │
            └──────────────┘

INBOUND FLOW (Existing - No Changes):
ECO SIT → ActiveGateway → EdgeConnect → Dynatrace SIT

OUTBOUND FLOW (New - Single IP Solution):
Dynatrace SIT → Istio Egress Gateway (34.123.45.67) → ActiveGateway → ECO SIT
```

### Detailed Component Architecture

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                        GKE CLUSTER DETAIL                                    │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│  ┌─────────────────────────────────────────────────────────────────────┐    │
│  │                    istio-system namespace                           │    │
│  │                                                                     │    │
│  │  ┌─────────────────┐    ┌─────────────────────────────────────┐    │    │
│  │  │ Istio Egress    │    │ LoadBalancer Service                │    │    │
│  │  │ Gateway Pod     │    │                                     │    │    │
│  │  │                 │    │ Name: istio-egressgateway-external  │    │    │
│  │  │ Port: 8443      │◄───┤ Type: LoadBalancer                  │    │    │
│  │  │ Port: 8080      │    │ LoadBalancerIP: 34.123.45.67        │    │    │
│  │  │                 │    │ Ports: 443, 80                     │    │    │
│  │  └─────────────────┘    └─────────────────────────────────────┘    │    │
│  │                                                                     │    │
│  │  ┌─────────────────────────────────────────────────────────────┐    │    │
│  │  │ ServiceEntry: eco-sit-external                              │    │    │
│  │  │ - Hosts: eco-sit.telus.internal                             │    │    │
│  │  │ - Ports: 443, 80                                            │    │    │
│  │  │ - Location: MESH_EXTERNAL                                   │    │    │
│  │  └─────────────────────────────────────────────────────────────┘    │    │
│  │                                                                     │    │
│  │  ┌─────────────────────────────────────────────────────────────┐    │    │
│  │  │ VirtualService: eco-sit-through-egress                      │    │    │
│  │  │ - Routes traffic to ECO SIT through egress gateway          │    │    │
│  │  │ - Ensures all outbound traffic uses single IP               │    │    │
│  │  └─────────────────────────────────────────────────────────────┘    │    │
│  └─────────────────────────────────────────────────────────────────────┘    │
│                                                                             │
│  ┌─────────────────────────────────────────────────────────────────────┐    │
│  │                 dynatrace-sit namespace                            │    │
│  │                                                                     │    │
│  │  ┌─────────────────┐    ┌─────────────────┐                        │    │
│  │  │ Dynatrace SIT   │    │ Istio Sidecar   │                        │    │
│  │  │ Application     │◄──►│ Proxy           │                        │    │
│  │  │ Pods            │    │ (Envoy)         │                        │    │
│  │  │                 │    │                 │                        │    │
│  │  └─────────────────┘    └─────────────────┘                        │    │
│  │                                   │                                 │    │
│  │                                   │ Routes ECO SIT calls            │    │
│  │                                   │ through egress gateway          │    │
│  │                                   ▼                                 │    │
│  │                         ┌─────────────────┐                        │    │
│  │                         │ Egress Gateway  │                        │    │
│  │                         │ (Single Static  │                        │    │
│  │                         │ IP: 34.123.45.67)                       │    │
│  │                         └─────────────────┘                        │    │
│  └─────────────────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────────────────┘
```

## Network Flow Details

### 1. Inbound Flow (Existing - No Changes)
```
ECO SIT API Call
    ↓
ActiveGateway (Telus routing)
    ↓
non-prod AVS Dynatrace EdgeConnect (secure tunnel)
    ↓
Dynatrace SIT in GKE (receives monitoring data/API calls)
```

### 2. Outbound Flow (New Implementation)
```
Dynatrace SIT needs to call ECO SIT API
    ↓
Istio Sidecar Proxy (intercepts outbound call)
    ↓
Routes to Istio Egress Gateway (based on ServiceEntry/VirtualService)
    ↓
Egress Gateway LoadBalancer Service (34.123.45.67)
    ↓
ECO SIT (receives call from whitelisted IP: 34.123.45.67)
```

