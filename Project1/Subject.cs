using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class clsModule // PROGRAMMING IN C#, SQL DATABASE DEVELOPMENT,...
    {
        string name;
        DateTime startTime; // 5:00PM
        DateTime endTime; // 9:00PM
        clsPerson instructor; // INSTRUCTORES ARE PEOPLE
        List<clsPerson> students; // SO ARE STUDENTS
        string classroomNumber; // B10 OR A01

        // WHAT CAN A SUBJECT DO? WHAT ACTIONS CAN IT PERFORM?
        public decimal calculatePercentageOfApprovedStudents()
        {
        //M SUFFIX IDENTIFIES THE NUMBER AS DECIMAL
            return 100.0M; 
        } 
        // CALCULATES SCORE PER INDIVIDUAL STUDENT
        public decimal calculteStudentScore(clsPerson student)
        {
           return 100.0M;
        }
    public void generateMocksStudentList()
        {
            // INSTANTIATE THE CLASS
            clsPerson newStudent = new clsPerson();
            // Fill in information in the properties
            newStudent.lastName = "snow";
            newStudent.firstName = "Jon";
            newStudent.age = 30;
            newStudent.eyeColor = "Black";
            newStudent.height = 1.75m;
            newStudent.idCard = "100-99-0-000";

            //WE CAN ALSO ASSIGN OBJECTS TO OTHER OBJECT`S PROPERTIES
            Guid uniqueIdentifier = new Guid();
            newStudent.UniqueId = uniqueIdentifier;

            newStudent.hobbies = new List<string>();
            // OR CALL METHODS/FUNCTIONS
            newStudent.takeTest("Programming in C#");
        }
    }
}
