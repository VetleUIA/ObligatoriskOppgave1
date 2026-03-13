// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

namespace obligatorisk_oppgave_1;

public class Program
{
    static List<Student> students = new List<Student>();
    static List<Employee> employees = new List<Employee>();
    static List<Course> courses = new List<Course>();
    static Library library = new Library();

    public static void Main(string[] args)
    {
        SeedData();

        bool running = true;

        while (running)
        {
            Console.WriteLine("universitetssystem");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student på kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Vis lånehistorikk for bok");
            Console.WriteLine("[0] Avslutt");
            Console.Write("Velg: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateCourse();
                    break;
                case "2":
                    EnrollStudentToCourse();
                    break;
                case "3":
                    PrintCoursesAndParticipants();
                    break;
                case "4":
                    SearchCourse();
                    break;
                case "5":
                    SearchBook();
                    break;
                case "6":
                    BorrowBook();
                    break;
                case "7":
                    ReturnBook();
                    break;
                case "8":
                    RegisterBook();
                    break;
                case "9":
                    ShowBookLoanHistory();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("Programmet avsluttes.");
                    break;
                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }

    static void SeedData()
    {
        students.Add(new Student("1001", "Vetle", "vetlehj@uia.no"));
        students.Add(new Student("1002", "Nora", "nora@uia.no"));
        students.Add(new ExchangeStudent(
            "1003",
            "Joe",
            "joe@uia.no",
            "Harward",
            "USA",
            new DateTime(2025, 6, 1),
            new DateTime(2026, 6, 1)
            ));

        employees.Add(new Employee("2345","Ola", "ola@uia.no", "foreleser", "IT"));
        employees.Add(new Employee("5432","Per", "per@uia.no", "Biblotekar", "Bibliotek"));

        courses.Add(new Course("IS110", "Programering", 10, 5));
        courses.Add(new Course("IS100", "Datasystemer", 10,4));

        library.RegisterBook(new Book("A1", "Koding for nye", "Ola Normann", 2005, 1));
        library.RegisterBook(new Book("A2", "Koding er gøy", "Vetle H. Jenssen", 2010, 1));

    }

    static void CreateCourse()
    {
        Console.Write("Kurskode:  ");
        string code = Console.ReadLine();

        Console.Write("Kursnavn:  ");
        string name = Console.ReadLine();

        Console.Write("Studiepoeng:  ");
        double credits = double.Parse(Console.ReadLine());

        Console.Write("Maks antall plasser:  ");
        int maxSeats = int.Parse(Console.ReadLine());

        courses.Add(new Course(code, name, credits, maxSeats));
        Console.WriteLine("Kurs opprettet.");
    }

    static void EnrollStudentToCourse()
    {
        Console.Write("StudentID: ");
        string studentId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(studentId))
        {
            Console.WriteLine("Du skrev ingen StudentID.");
            return;
        }

        studentId = studentId.Trim();

        Student student = students.FirstOrDefault(s => s.StudentId == studentId);

        if (student == null)
        {
            Console.WriteLine("Fant ikke student.");
            return;
        }

        Console.Write("Kurskode: ");
        string courseCode = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(courseCode))
        {
            Console.WriteLine("Du skrev ingen kurskode.");
            return;
        }

        courseCode = courseCode.Trim();

        Course course = courses.FirstOrDefault(c => c.Code == courseCode);

        if (course == null)
        {
            Console.WriteLine("Fant ikke kurs.");
            return;
        }

        bool success = course.EnrollStudent(student);

        if (success)
            Console.WriteLine("Student meldt på kurs.");
        else
            Console.WriteLine("Kurset er fullt eller studenten er allerede meldt på.");
    }
    static void PrintCoursesAndParticipants()
    {
        foreach (Course course in courses)
        {
            Console.WriteLine($"\n{course.Code} - {course.Name}");

            if (course.Participants.Count == 0)
            {
                Console.WriteLine("Ingen deltagere.");
            }
            else
            {
                foreach (Student student in course.Participants)
                {
                    Console.WriteLine($"- {student.Name} ({student.StudentId})");
                }
            }
        }
    }

    static void SearchCourse()
    {
        Console.Write("Søk etter kurskode eller navn: ");
        string searchTerm = Console.ReadLine().ToLower();

        List<Course> result = courses
            .Where(c => c.Code.ToLower().Contains(searchTerm) || c.Name.ToLower().Contains(searchTerm))
            .ToList();

        if (result.Count == 0)
        {
            Console.WriteLine("Ingen kurs funnet.");
            return;
        }

        foreach (Course course in result)
        {
            Console.WriteLine($"{course.Code} - {course.Name}, Studiepoeng: {course.Credits}");
        }
    }

    static void SearchBook()
    {
        Console.Write("Søk etter bok: ");
        string searchTerm = Console.ReadLine();

        List<Book> result = library.SearchBooks(searchTerm);

        if (result.Count == 0)
        {
            Console.WriteLine("Ingen bøker funnet.");
            return;
        }

        foreach (Book book in result)
        {
            Console.WriteLine($"{book.Id} - {book.Title} av {book.Author} ({book.Year})");
        }
    }

    static void BorrowBook()
    {
        Console.Write("Bok-ID: ");
        string bookId = Console.ReadLine();

        User borrower = FindUser();

        if (borrower == null)
        {
            Console.WriteLine("Fant ikke bruker.");
            return;
        }

        bool success = library.BorrowBook(bookId, borrower);

        if (success)
            Console.WriteLine("Boken blir lånt ut.");
        else
            Console.WriteLine("Boken er utlånt til en annen person.");
    }

    static void ReturnBook()
    {
        Console.Write("Bok-ID: ");
        string bookId = Console.ReadLine();

        User borrower = FindUser();

        if (borrower == null)
        {
            Console.WriteLine("Fant ikke bruker.");
            return;
        }

        bool success = library.ReturnBook(bookId, borrower);

        if (success)
            Console.WriteLine("Boken ble levert inn.");
        else
            Console.WriteLine("Fant ikke aktivt lån.");
    }

    static void RegisterBook()
    {
        Console.Write("Bok-ID: ");
        string id = Console.ReadLine();

        Console.Write("Tittel: ");
        string title = Console.ReadLine();

        Console.Write("Forfatter: ");
        string author = Console.ReadLine();

        Console.Write("År: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Antall eksemplarer: ");
        int totalCopies = int.Parse(Console.ReadLine());

        Book book = new Book(id, title, author, year, totalCopies);
        library.RegisterBook(book);

        Console.WriteLine("Bok registrert.");
    }

    static User FindUser()
    {
        Console.Write("Er låner student eller ansatt? (s/e): ");
        string type = Console.ReadLine().ToLower();

        if (type == "s")
        {
            Console.Write("StudentID: ");
            string studentId = Console.ReadLine();
            return students.FirstOrDefault(s => s.StudentId == studentId);
        }
        else if (type == "e")
        {
            Console.Write("AnsattID: ");
            string employeeId = Console.ReadLine();
            return employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        return null;
    }
    
    static void ShowBookLoanHistory()
    {
        Console.Write("Bok-ID: ");
        string bookId = (Console.ReadLine() ?? "").Trim();

        List<Loan> history = library.GetLoanHistoryForBook(bookId);

        if (history.Count == 0)
        {
            Console.WriteLine("Ingen lånehistorikk funnet for denne boken.");
            return;
        }

        Console.WriteLine("Lånehistorikk:");
        foreach (Loan loan in history)
        {
            string status;

            if (loan.IsActive)
                status = "Aktivt lån";
            else
                status = $"Returnert: {loan.ReturnDate}";

            Console.WriteLine($"Bruker: {loan.Borrower.Name}, Lånt: {loan.LoanDate}, {status}");
        }
    }

}



