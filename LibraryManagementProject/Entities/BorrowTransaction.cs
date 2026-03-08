using LibraryManagementProject.Contracts;

namespace LibraryManagementProject.Entities;

/// <summary>
/// This class responsible for managing borrow transactions
/// including the due data, return data, calculate the fines
/// </summary>
public class BorrowTransaction : IDisplayable
{
    private static int _counter = 1000;

    private const decimal FinePerDay = 10m;

    public int TransactionId { get; private set; }

    public Member Member { get; set; } = null!;
    public BookCopy BookCopy { get; set; } = null!;

    public DateOnly BorrowDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
    public BorrowTransaction(Member member, BookCopy bookCopy, int loanDays)
    {
        TransactionId = ++_counter;
        Member = member;
        BookCopy = bookCopy;
        BorrowDate = DateOnly.FromDateTime(DateTime.Today);
        DueDate = DateOnly.FromDateTime(DateTime.Today.AddDays(loanDays));
        ReturnDate = null;
    }

    public bool IsReturned() => ReturnDate.HasValue;

    public void MarkReturned(DateOnly returnDate)
        => ReturnDate = returnDate;



    public decimal CalculateFine()
    {
        DateOnly effective = ReturnDate ?? DateOnly.FromDateTime(DateTime.Today);
        int overDueDays = effective.DayNumber - DueDate.DayNumber;

        return overDueDays > 0 ? overDueDays * FinePerDay : 0;
    }

    public string DisplayInformations()
    {
        var returned = ReturnDate?.ToString("dd/MM/yyyy") ?? "Not returned yet";
        var status = IsReturned() ? "Returned" : "Active";
        var fine = CalculateFine();

        return $"""
           ── Transaction #{TransactionId} ──────────────
           Book : {BookCopy.Book.Title}
           Copy ID : {BookCopy.CopyId}
           Borrowed : {BorrowDate:dd/MM/yyyy}
           Due : {DueDate:dd/MM/yyyy}
           Returned : {returned}
           Status : {status}
           Fine : {(fine == 0 ? "None" : fine)}
           """;
    }
}
