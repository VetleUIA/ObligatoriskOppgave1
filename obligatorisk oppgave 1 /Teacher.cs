

using System.Collections.Generic;

namespace obligatorisk_oppgave_1;

public class Teacher : Employee
{
    public List<Course> TeachingCourses { get; set; }

    public Teacher(string employeeId, string name, string email, string username, string password, string department)
        : base(employeeId, name, email, username, password, "Faglærer", department)
    {
        TeachingCourses = new List<Course>();
    }
}