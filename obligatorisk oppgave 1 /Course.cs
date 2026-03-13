using System.Collections.Generic;
using System.Linq;

namespace obligatorisk_oppgave_1;

public class Course
{
    public string Code  { get; set; }
    public string Name { get; set; }
    public double Credits { get; set; }
    public int MaxSeats { get; set; }
    
    public List<Student> Participants { get; set; }

    public Course(string code, string name, double credits, int maxSeats)
    {
        Code = code;
        Name = name;
        Credits = credits;
        MaxSeats = maxSeats; 
        Participants = new List<Student>();
    }

    public bool EnrollStudent(Student student)
    {
        if (Participants.Count >= MaxSeats)
            return false;

        if (Participants.Any(s => s.StudentId == student.StudentId))
            return false;

        Participants.Add(student);
        student.EnrolledCourses.Add(this);
        return true;
    }

    public bool RemoveStudent(Student student)
    {
        if (!Participants.Contains(student))
            return false;
        
        Participants.Remove(student);
        student.EnrolledCourses.Remove(this);
        return true;
    }
}