using LibraryManagementProject.Contracts;
using LibraryManagementProject.Enums;

namespace LibraryManagementProject.Entities;

// Borrow
// IsAvaible
// Return
public class BookCopy : IDisplayable, IBorrowable
{

    public string CopyId { get; set; } = null!;

    public string Condition { get; set; } = null!;

    public CopyStatus Status { get; set;  }

    public Book Book { get; set; } = null!;

    public BorrowTransaction? ActiveBorrowTransaction { get; set; } 
    
    public BookCopy(string copyId, string condition, CopyStatus copyStatus, Book book)
    {
        CopyId = copyId;
        Condition = condition;
        Status = copyStatus;
        Book = book;
    }

    public void Borrow(Member member, int loanDays = 14)
    {
        if(!IsAvailable())
            throw new InvalidOperationException($"Copy {CopyId} is not available (status = {Status})");

        Status = CopyStatus.Borrowed;

        ActiveBorrowTransaction = new BorrowTransaction(member, this, loanDays);

        member.AddBorrowTransaction(ActiveBorrowTransaction);
    }

    public bool IsAvailable()
        => Status == CopyStatus.Available;

    public decimal Return()
    {
        if(Status is not CopyStatus.Borrowed)
            throw new InvalidOperationException("Copy is not currently borrowed.");

        ActiveBorrowTransaction!.MarkReturned(DateOnly.FromDateTime(DateTime.Today));

        decimal fine = ActiveBorrowTransaction.CalculateFine();

        Status = CopyStatus.Available;

        ActiveBorrowTransaction = null;

        return fine;
    }

    public string DisplayInformations()
      => $"Copy [{CopyId}] - {Book.Title} | Condition: {Condition} | {Status}";
}
