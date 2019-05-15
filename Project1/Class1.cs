using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class clsPerson
    {
        // ALL PROPERTIES AND METHODS HERE
        public string firstName { get; set; } // JOHN
        public string lastName { get; set; } // DOE
        public int age { get; set; } // 31
        public decimal height { get; set; } // BECAUSE WE ARE USING METERS e.g 1.75
        public string eyeColor { get; set; } // CAN BE BLACK, BROWN, GREEN, BLUE...
        public string idCard { get; set; } // SOMETHING LIKE A290-08SD-088S
        public Guid UniqueId { get; set; } // SOMETHING MORE COMPLEX... '2837B519-198E-4F08-9825-1080657844DB'
        public List<string> hobbies { get; set; } // BECAUSE YOU CAN HAVE MANY

        // ALL METHODS/FUNCTIONS HERE - Think about a School System
        public void registerAsNewStudent(decimal paydAmount, string programName)
        { 
            // CODE GOES HERE 
        }
        public void takeTest(string className)
        {
            // CODE GOES HERE
        }
        public void printReport()
        {
            // CODE GOES HERE
        }
    }
}
