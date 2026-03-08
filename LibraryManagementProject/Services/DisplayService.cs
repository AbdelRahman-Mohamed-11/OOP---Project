using ConsoleTheme;
using LibraryManagementProject.Entities;

namespace LibraryManagementProject.Services
{
    public class DisplayService
    {
        public void ShowBranchInfo(LibraryBranch branch)
        {
            ThemeHelper.PrintHeader("LIBRARY BRANCH INFO");
            Console.WriteLine(branch.DisplayInformations());
        }

        public void ShowAllUsers(LibraryBranch branch)
        {
            ThemeHelper.PrintHeader("ALL REGISTERED USERS");

            IReadOnlyList<User> users = branch.Users;

            for (int i = 0; i < users.Count; i++)
            {
                string header =
                    users[i] is Librarian
                    ? "LIBRARIAN PROFILE"
                    : "MEMBER PROFILE";

                ThemeHelper.PrintSectionTitle(header);
                Console.WriteLine(users[i].DisplayInformations());
            }
        }

        public void ShowAvailableCopies(LibraryBranch branch)
        {
            ThemeHelper.PrintHeader("AVAILABLE BOOK COPIES");

            List<BookCopy> available = branch.GetAvailableBookCopies();

            if (available.Count == 0)
            {
                ThemeHelper.PrintWarning("No available book copies found.");
                return;
            }

            for (int i = 0; i < available.Count; i++)
                Console.WriteLine(available[i].DisplayInformations());
        }

        public void ShowAllCopies(LibraryBranch branch)
        {
            ThemeHelper.PrintHeader("ALL BOOK COPIES");

            if (branch.BookCopies.Count == 0)
            {
                ThemeHelper.PrintWarning("No book copies found.");
                return;
            }

            for (int i = 0; i < branch.BookCopies.Count; i++)
                Console.WriteLine(branch.BookCopies[i].DisplayInformations());
        }

        public void ShowMemberHistory(Member member)
        {
            ThemeHelper.PrintSectionTitle($"Borrowing History for {member.Name}:");
            Console.WriteLine(member.GetTransactionsHistory());
        }

        public void ShowBorrowSuccess(BookCopy copy, Member member)
        {
            ThemeHelper.PrintSuccess(
                $"Copy [{copy.CopyId}] \"{copy.Book.Title}\" borrowed by {member.Name}."
            );

            ThemeHelper.PrintSuccess(
                $"Due date: {copy.ActiveBorrowTransaction!.DueDate:dd/MM/yyyy}"
            );
        }

        public void ShowReturnSuccess(BookCopy copy, decimal fine)
        {
            ThemeHelper.PrintSuccess(
                $"Copy [{copy.CopyId}]: {copy.Book.Title} returned."
            );

            if (fine > 0)
                ThemeHelper.PrintWarning($"Late return fine: {fine:F2} EGP");
            else
                ThemeHelper.PrintSuccess("Returned on time. No fine.");
        }

        public void ShowRegistrationSuccess(Member member)
        {
            ThemeHelper.PrintSuccess(
                $"Member: {member.Name} - [{member.Id}] registered successfully."
            );
        }

        public void ShowAddCopySuccess(BookCopy copy)
        {
            ThemeHelper.PrintSuccess(
                $"Copy [{copy.CopyId}] - {copy.Book.Title}: added."
            );
        }
    }
}