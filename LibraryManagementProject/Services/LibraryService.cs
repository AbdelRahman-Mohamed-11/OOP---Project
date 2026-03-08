using ConsoleTheme;
using LibraryManagementProject.Entities;

namespace LibraryManagementProject.Services
{
    public class LibraryService(LibraryBranch branch, DisplayService display)
    {
        private readonly LibraryBranch _branch = branch;
        private readonly DisplayService _display = display;

        public void HandleBorrow()
        {
            string memberId = ThemeHelper.Prompt("Member ID").NormalizeId();
            Member member = _branch.FindMemberById(memberId);

            _display.ShowAvailableCopies(_branch);

            string copyId = ThemeHelper.Prompt("Copy ID to borrow").NormalizeId();
            BookCopy copy = _branch.FindCopyByCopyId(copyId);

            copy.Borrow(member);

            _display.ShowBorrowSuccess(copy, member);
        }

        public void HandleReturn()
        {
            string copyId = ThemeHelper.Prompt("Copy ID to return").NormalizeId();
            BookCopy copy = _branch.FindCopyByCopyId(copyId);

            decimal fine = copy.Return();
            _display.ShowReturnSuccess(copy, fine);
        }

        public void HandleHistory()
        {
            string memberId = ThemeHelper.Prompt("Member ID").NormalizeId();
            Member member = _branch.FindMemberById(memberId);

            _display.ShowMemberHistory(member);
        }

        public void HandleRegisterMember()
        {
            string name = ThemeHelper.Prompt("Full Name");

            string phone = ThemeHelper.Prompt("Phone Number");
            
            if (!phone.ContainDigit())
                throw new InvalidOperationException(
                    "Phone number must contain at least one digit."
                );

            string email = ThemeHelper.Prompt("Email Address");
            
            if (!email.IsValidEmail())
                throw new InvalidOperationException(
                    "Invalid email format. Must contain '@' and domain."
                );

            Member member = _branch.RegisterMember(name, phone);
            _display.ShowRegistrationSuccess(member);
        }
    }
}