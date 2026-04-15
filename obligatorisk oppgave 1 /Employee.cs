

namespace obligatorisk_oppgave_1;

public class Employee : User
{
    public string EmployeeId { get; set; }
    public string Position { get; set; }
    public string Department { get; set; }

    public Employee(string employeeId, string name, string email, string username, string password, string position, string department)
        : base(name, email,  username, password)
    {
        EmployeeId = employeeId;
        Position = position;
        Department = department;
    }

    public override string GetUserKey()
    {
        return "E" + EmployeeId;
    }
}