// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;

namespace obligatorisk_oppgave_1;
// forklaring:  
public class Program
{
    static List<User> users = new List<User>();
    static List<Student> students = new List<Student>();
    static List<Employee> employees = new List<Employee>();
    static List<Teacher> teachers = new List<Teacher>();
    static List<Course> courses = new List<Course>();
    static Library library = new Library();
    

    public static void Main(string[] args)
    {
        SeedData();

        bool running = true;

        while (true)
        {
            Console.WriteLine("universitetssystem");
            Console.WriteLine("------Startmeny----");
            Console.WriteLine("[1] Eksisterende bruker");
            Console.WriteLine("[2] Ny bruker");
            Console.WriteLine("[0] Avslutt");
            Console.Write("Velg: ");
            string choice = (Console.ReadLine() ?? "").Trim();

            if (choice == "1")
            {
                User loggedInUser = LoginUser();
                if (loggedInUser != null)
                    OpenRoleMenu(loggedInUser);
            }
            else if (choice == "2")
            {
                RegisterUser();
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Ugyldig valg.");
            }
        }


        static void RegisterUser()
        {
            Console.WriteLine("Velg Rolle");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Biblotekar");
            Console.WriteLine("[3] Faglærer");
            Console.Write("Valg: ");
            string roleChoice = (Console.ReadLine() ?? "").Trim();

            Console.Write("Navn: ");
            string name = (Console.ReadLine() ?? "").Trim();

            Console.Write("E-post: ");
            string email = (Console.ReadLine() ?? "").Trim();

            Console.Write("Brukernavn: ");
            string username = (Console.ReadLine() ?? "").Trim();

            Console.Write("Passord: ");
            string password = (Console.ReadLine() ?? "").Trim();

            if (users.Any(u => u.Username == username))
            {
                Console.WriteLine("Brukernavnet finnes allerede.");
                return;
            }

            if (roleChoice == "1")
            {
                Console.Write("StudentID: ");
                string studentId = (Console.ReadLine() ?? "").Trim();

                Student student = new Student(studentId, name, email, username, password);
                users.Add(student);
                students.Add(student);

                Console.WriteLine("student registrert");
            }
            else if (roleChoice == "2")
            {
                Console.Write("AnsattID: ");
                string ansattId = (Console.ReadLine() ?? "").Trim();

                Employee librarian = new Employee(ansattId, name, email, username, password, "Biblotekar", "biblotek");
                users.Add(librarian);
                employees.Add(librarian);

                Console.WriteLine("Biblotekansatt registrert");
            }
            else if (roleChoice == "3")
            {
                Console.Write("AnsattID: ");
                string teacherId= (Console.ReadLine() ?? "").Trim();

                Teacher teacher = new Teacher(teacherId, name, email, username, password, "IT");
                users.Add(teacher);
                teachers.Add(teacher);
                employees.Add(teacher);

                Console.WriteLine("Faglærer registrert");
            }
            else
            {
                Console.WriteLine("Ugyldig valg.");
            }
        }

        static User LoginUser()
        {
            Console.Write("Brukernavn: ");
            string username = (Console.ReadLine() ?? "").Trim();

            Console.Write("Passord: ");
            string password = (Console.ReadLine() ?? "").Trim();

            User user = users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Feil Brukernavn eller passord.");
                return null;
            }

            Console.WriteLine($"Velkommen, {user.Name}!");
            return user;
        }

        static void OpenRoleMenu(User user)
                {
                    if (user is Student student)
                    {
                        StudentMenu(student);
                    }
                    else if (user is Teacher teacher)
                    {
                        TeacherMenu(teacher);
                    }
                    else if (user is Employee employee && employee.Position == "Bibliotekar")
                    {
                        LibrarianMenu(employee);
                    }
                }
                
                static void StudentMenu(Student student)
                {
                    bool running = true;

                    while (running)
                    {
                        Console.WriteLine("\n--- Studentmeny ---");
                        Console.WriteLine("[1] Meld på kurs");
                        Console.WriteLine("[2] Meld av kurs");
                        Console.WriteLine("[3] Se mine kurs");
                        Console.WriteLine("[4] Se mine karakterer");
                        Console.WriteLine("[5] Søk på bok");
                        Console.WriteLine("[6] Lån bok");
                        Console.WriteLine("[7] Returner bok");
                        Console.WriteLine("[0] Logg ut");
                        Console.Write("Velg: ");

                        string choice = (Console.ReadLine() ?? "").Trim();

                        switch (choice)
                        {
                            case "1":
                                EnrollStudentToCourse(student);
                                break;
                            case "2":
                                UnenrollStudentFromCourse(student);
                                break;
                            case "3":
                                ShowStudentCourses(student);
                                break;
                            case "4":
                                ShowStudentGrades(student);
                                break;
                            case "5":
                                SearchBook();
                                break;
                            case "6":
                                BorrowBook(student);
                                break;
                            case "7":
                                ReturnBook(student);
                                break;
                            case "0":
                                running = false;
                                break;
                            default:
                                Console.WriteLine("Ugyldig valg.");
                                break;
                        }
                    }
                }
                
                static void TeacherMenu(Teacher teacher)
                {
                    bool running = true;

                    while (running)
                    {
                        Console.WriteLine("\n--- Faglærermeny ---");
                        Console.WriteLine("[1] Opprett kurs");
                        Console.WriteLine("[2] Søk på kurs");
                        Console.WriteLine("[3] Søk på bok");
                        Console.WriteLine("[4] Lån bok");
                        Console.WriteLine("[5] Returner bok");
                        Console.WriteLine("[6] Sett karakter");
                        Console.WriteLine("[7] Registrer pensum");
                        Console.WriteLine("[0] Logg ut");
                        Console.Write("Velg: ");

                        string choice = (Console.ReadLine() ?? "").Trim();

                        switch (choice)
                        {
                            case "1":
                                CreateCourse(teacher);
                                break;
                            case "2":
                                SearchCourse();
                                break;
                            case "3":
                                SearchBook();
                                break;
                            case "4":
                                BorrowBook(teacher);
                                break;
                            case "5":
                                ReturnBook(teacher);
                                break;
                            case "6":
                                SetGradeForStudent(teacher);
                                break;
                            case "7":
                                AddCurriculumToCourse(teacher);
                                break;
                            case "0":
                                running = false;
                                break;
                            default:
                                Console.WriteLine("Ugyldig valg.");
                                break;
                        }
                    }
                }
                
                static void LibrarianMenu(Employee librarian)
                {
                    bool running = true;

                    while (running)
                    {
                        Console.WriteLine("\n--- Bibliotekmeny ---");
                        Console.WriteLine("[1] Registrer bok");
                        Console.WriteLine("[2] Se aktive lån");
                        Console.WriteLine("[3] Se historikk");
                        Console.WriteLine("[0] Logg ut");
                        Console.Write("Velg: ");

                        string choice = (Console.ReadLine() ?? "").Trim();

                        switch (choice)
                        {
                            case "1":
                                RegisterBook();
                                break;
                            case "2":
                                ShowActiveLoans();
                                break;
                            case "3":
                                ShowLoanHistory();
                                break;
                            case "0":
                                running = false;
                                break;
                            default:
                                Console.WriteLine("Ugyldig valg.");
                                break;
                        }
                    }
                }

            }
            
            
            static void SeedData()       
     {
        Student s1 =new Student("1001", "Vetle", "vetlehj@uia.no", "VetleUIA", "Vetle123");
        Student s2 =new Student("1002", "Nora", "nora@uia.no", "NoraUIA", "Nora123");
        
        students.Add(new ExchangeStudent(
            "1003",
            "Joe",
            "joe@uia.no",
            "JoeUIA",
            "Joe123",
            "harward",
            "USA",
            new DateTime(2025, 6, 1),
            new DateTime(2026, 6, 1)
            ));
        
         Employee librarian = new Employee("5432","Per", "per@uia.no", "PerUIA", "Per123", "Bibliotekar", "bibliotek");

         Teacher teacher1 = new Teacher("2345","Ola", "ola@uia.no", "OlaUIA","Ola123", "IT");
            
         students.Add(s1);
         students.Add(s2);

         employees.Add(librarian);
         employees.Add(teacher1);

         teachers.Add(teacher1);

         users.Add(s1);
         users.Add(s2);
         users.Add(librarian); 
         users.Add(teacher1);
            
        Course c1 = new Course("IS110", "Programering", 10, 5, teacher1);
        Course c2 = new Course("IS100", "Datasystemer", 10,4, teacher1);

        library.RegisterBook(new Book("A1", "Koding for nye", "Ola Normann", 2005, 1));
        library.RegisterBook(new Book("A2", "Koding er gøy", "Vetle H. Jenssen", 2010, 1));
        
        courses.Add(c1);
        courses.Add(c2);

        teacher1.TeachingCourses.Add(c1);
        teacher1.TeachingCourses.Add(c2);

    }

    static void CreateCourse(Teacher teacher)
    {
        Console.Write("Kurskode:  ");
        string code = Console.ReadLine();

        Console.Write("Kursnavn:  ");
        string name = Console.ReadLine();

        Console.Write("Studiepoeng:  ");
        double credits = double.Parse(Console.ReadLine());

        Console.Write("Maks antall plasser:  ");
        int maxSeats = int.Parse(Console.ReadLine());


        if (courses.Any(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase) ||
                             c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Kurs med samme kode eller navn finnes allerde");
            return;
        }
        
        Course course = new Course(code, name, credits, maxSeats, teacher);
        courses.Add(course);
        teacher.TeachingCourses.Add(course);
    }

    static void EnrollStudentToCourse(Student student)
    {
        Console.WriteLine("kurskode: ");
        string code = (Console.ReadLine() ?? "").Trim();
        
        Course course = courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

        if (course == null)
        {
            Console.WriteLine("Fant ikke kurs");
            return;
        }
        
        bool success = course.EnrollStudent(student);
        
        if (success)
            Console.WriteLine("Student meldt på kurs.");
        else 
            Console.WriteLine(" kunne ikke melde student på kurs, kurs er enten fult eller student er meldt på fra før");
    }

    static void UnenrollStudentFromCourse(Student student)
    {
        Console.WriteLine("kurskode: ");
        string code = (Console.ReadLine() ?? "").Trim();
        
        Course course = courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

        if (course == null)
        {
            Console.WriteLine("Fant ikke kurs");
            return;
        }
        bool success = course.RemoveStudent(student);

        if (success)
            Console.WriteLine("Student meldt av kurs");
        else
        {
            Console.WriteLine("Studenten er ikke meldt på kurset.");
        }
    }

    static void ShowStudentCourses(Student student)
    {
        if (student.EnrolledCourses.Count == 0)
        {
            Console.WriteLine("Du er ikke meldt på noen kurs");
            return;
        }

        foreach (Course course in student.EnrolledCourses)
        {
            Console.WriteLine($"{course.Code} - {course.Name}");
        }
    }

    static void ShowStudentGrades(Student student)
    {
        if (student.Grades.Count == 0)
        {
            Console.WriteLine("ingen karakterer registret.");
            return;
        }
        
        foreach (var grade in student.Grades)
        {
            Console.WriteLine($"kurs: {grade.Key},Karakter: {grade.Value} ");
        }
    }

    static void SetGradeForStudent(Teacher teacher)
    {
        Console.Write("kurs kode");
        string courseCode = (Console.ReadLine() ?? "").Trim();
        
        Course course = teacher.TeachingCourses
            .FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));
        
        if (course == null)
            {
            Console.WriteLine("du underviser ikke dette kurset");
            return;
            }
        Console.Write("StudentID: ");
        string studentId = (Console.ReadLine() ?? "").Trim();

        Student student = course.Participants.FirstOrDefault(s => s.StudentId == studentId);

        if (student == null)
        {
            Console.WriteLine("Fant ikke student i kurset");
            return; 
        }
        
        Console.Write("Krakterer: ");
        string grade  = (Console.ReadLine() ?? "").Trim();
        
        bool success = course.SetGrades(student, grade);
        
        if (success)
            Console.WriteLine("Karakter satt");
        else 
            Console.WriteLine("kunne ikke sette karakter");
    }

    static void AddCurriculumToCourse(Teacher teacher)
    {
        Console.Write("kurs kode");
        string courseCode = (Console.ReadLine() ?? "").Trim();
        
        Course course = teacher.TeachingCourses
            .FirstOrDefault(c => c.Code.Equals(courseCode, StringComparison.OrdinalIgnoreCase));

        if (course == null)
        {
            Console.WriteLine("Du underviser ikke kurset");
            return;
        }
        
        Console.Write("pensumtekst: ");
        string item = (Console.ReadLine() ?? "").Trim();
        
        course.AddCurriculum(item);
        Console.WriteLine("Pensum registrert.");
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

    static void BorrowBook(User borrower)
    {
        Console.Write("Bok-ID: ");
        string bookId = (Console.ReadLine() ?? "").Trim();

        bool success = library.BorrowBook(bookId, borrower);

        if (success)
            Console.WriteLine("Boken blir lånt ut.");
        else
            Console.WriteLine("Kunne ikke låne ut boken.");
    }

    static void ReturnBook(User borrower)
    {
        Console.Write("Bok-ID: ");
        string bookId = Console.ReadLine();

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

    static void ShowActiveLoans()
    {
        List<Loan> activeLoans = library.GetActiveLoans();

        if (activeLoans.Count == 0)
        {
            Console.WriteLine("ingen aktive lån");
            return;
        }

        foreach (Loan loan in activeLoans)
        {
            Console.WriteLine($"bok: {loan.Book.Title}, Låner: {loan.Borrower.Name}, Dato: {loan.LoanDate}");
        }
    }

    static void ShowLoanHistory()
    {
        List<Loan> history = library.GetLoanHistory();

        if (history.Count == 0)
        {
            Console.WriteLine("Ingen lånehistorikk.");
            return;
        }

        foreach (Loan loan in history)
        {
            string status = loan.IsActive ? "Aktiv" : $"Returnert: {loan.ReturnDate}";
            Console.WriteLine($"Bok: {loan.Book.Title},Låner: {loan.Borrower.Name}, {status}");
        }
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



