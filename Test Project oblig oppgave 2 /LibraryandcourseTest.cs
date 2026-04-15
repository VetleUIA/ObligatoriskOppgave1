
using Xunit;
using obligatorisk_oppgave_1;

namespace Test_Project_oblig_oppgave_2;

public class LibraryandcourseTest
{
    [Fact]
    public void EnrollStudent_ShouldAddStudentToCourse()
    {
        Teacher teacher = new Teacher("2345", "Ola", "ola@uia.no", "ola", "1234", "IT");
        Course course = new Course("IS110", "Programmering", 10, 2, teacher);
        Student student = new Student("1001", "Nora", "nora@uia.no", "nora", "1234");

        bool result = course.EnrollStudent(student);

        Assert.True(result);
        Assert.Contains(student, course.Participants);
        Assert.Contains(course, student.EnrolledCourses);
    }

    [Fact]
    public void EnrollStudent_ShouldReturnFalse_WhenCourseIsFull()
    {
        Teacher teacher = new Teacher("2345", "Ola", "ola@uia.no", "ola", "1234", "IT");
        Course course = new Course("IS110", "Programmering", 10, 1, teacher);
        Student student1 = new Student("1001", "Nora", "nora@uia.no", "nora", "1234");
        Student student2 = new Student("1002", "Vetle", "vetle@uia.no", "vetle", "1234");

        course.EnrollStudent(student1);
        bool result = course.EnrollStudent(student2);

        Assert.False(result);
        Assert.Single(course.Participants);
        Assert.DoesNotContain(student2, course.Participants);
    }

    [Fact]
    public void RemoveStudent_ShouldRemoveStudentFromCourse()
    {
        Teacher teacher = new Teacher("2345", "Ola", "ola@uia.no", "ola", "1234", "IT");
        Course course = new Course("IS110", "Programmering", 10, 2, teacher);
        Student student = new Student("1001", "Nora", "nora@uia.no", "nora", "1234");

        course.EnrollStudent(student);
        bool result = course.RemoveStudent(student);

        Assert.True(result);
        Assert.DoesNotContain(student, course.Participants);
        Assert.DoesNotContain(course, student.EnrolledCourses);
    }
}
