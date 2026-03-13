
using System.Collections.Generic;
using System.Linq;
using System;

namespace obligatorisk_oppgave_1;

public class Library
{
    public List<Book> Books { get; set; } = new();
    public List<Loan> Loans { get; set; } = new();
    
    //registrere bok
    public void RegisterBook(Book book)
    {
        Books.Add(book);
    }
    
    //søke etter bok
    public List<Book> SearchBooks(string searchTerm)
    {
        searchTerm = searchTerm.ToLower();

        return Books.Where(b =>b.Id.ToLower().Contains(searchTerm) || 
                                b.Title.ToLower().Contains(searchTerm) ||
                                b.Author.ToLower().Contains(searchTerm))
            .ToList();
    }
    
    // Låne en bok 
    
    public bool BorrowBook(string bookId, User borrower)
    {
        var book = Books.FirstOrDefault(b => b.Id == bookId);
        if (book == null)
            return false; 
        
        int activeLoans = Loans.Count(I => I.Book.Id == bookId && I.IsActive);

        if (activeLoans >= book.TotalCopies)
            return false; 
        
        Loans.Add(new Loan(book, borrower));
        return true; 
    }
    
    //returnere bok

    public bool ReturnBook(string bookId, User borrower)
    {
        var loan = Loans.FirstOrDefault(I =>
            I.Book.Id == bookId &&
            I.Borrower == borrower && 
            I.IsActive);

        if (loan == null)
            return false;
        
        loan.ReturnBook();
        return true; 
    }
    //historikk for lån
    public List<Loan> GetActiveLoans()
    {
        return Loans.Where(I => I.IsActive).ToList();
    }
    public List<Loan> GetLoanHistory()
    {
        return Loans;
    }
    
    public List<Loan> GetLoanHistoryForBook(string bookId)
    {
        return Loans
            .Where(l => l.Book.Id == bookId)
            .ToList();
    }
}