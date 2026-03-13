using System.Collections.Generic;

namespace obligatorisk_oppgave_1;

public class Student : User 
{
    public string StudentId {get; set;}
    public List<Course> EnrolledCourses {get; set;}

    public Student(string studentId, string name, string email)
        : base(name, email)

    {
        StudentId = studentId;
        EnrolledCourses = new List<Course>();
    }

    public override string GetUserKey()
    {
        return "S" + StudentId; 
    }
}