using LibraryManagementProject.Entities;

namespace LibraryManagementProject.Contracts;

public interface IBorrowable
{
    bool IsAvailable();

    void Borrow(Member member, int loanDays = 14);

    decimal Return();
}
