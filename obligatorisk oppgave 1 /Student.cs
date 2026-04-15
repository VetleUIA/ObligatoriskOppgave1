using System.Collections.Generic;

namespace obligatorisk_oppgave_1;

public class Student : User 
{
    public string StudentId {get; set;}
    public List<Course> EnrolledCourses {get; set;}
    public Dictionary<string, string> Grades {get; set;}

    public Student(string studentId, string name, string email, string username, string password)
        : base(name, email,  username, password)

    {
        StudentId = studentId;
        EnrolledCourses = new List<Course>();
        Grades = new Dictionary<string, string>();
    }

    public override string GetUserKey()
    {
        return "S" + StudentId; 
    }
}