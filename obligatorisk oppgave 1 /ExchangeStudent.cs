using System;

namespace obligatorisk_oppgave_1;

public class ExchangeStudent : Student
{
    public string HomeUniversity { get; set; }
    public string Country { get; set; }
    public DateTime PeriodFrom { get; set; }
    public DateTime PeriodTo { get; set; }

    public ExchangeStudent(
        string studentId,
        string name,
        string email,
        string username,
        string password,
        string homeUniversity,
        string country,
        DateTime periodFrom,
        DateTime periodTo)
        : base(studentId, name, email, username, password)
    {
        HomeUniversity = homeUniversity;
        Country = country;
        PeriodFrom = periodFrom;
        PeriodTo = periodTo;
    }
}