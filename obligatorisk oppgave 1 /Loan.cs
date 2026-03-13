using System;

namespace obligatorisk_oppgave_1;

public class Loan
{
    public Book Book { get; set; }
    
    public User Borrower { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public bool IsActive => ReturnDate == null;
    
    public Loan(Book book, User borrower)
    { 
        Book = book;
        Borrower = borrower; 
        LoanDate = DateTime.Now;
    }

    public void ReturnBook()
    {
        ReturnDate = DateTime.Now;
    }

}