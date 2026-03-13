namespace obligatorisk_oppgave_1;

public class Book
{
    public string Id {get; set;}
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int TotalCopies  { get; set; }
    
    public Book(string id, string title, string author, int year, int totalCopies)
        
        {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        TotalCopies = totalCopies;
        }
}