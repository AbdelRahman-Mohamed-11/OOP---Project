using LibraryManagementProject.Entities;
using LibraryManagementProject.Enums;

namespace LibraryManagementProject.Seeder
{
    internal static class DataSeeder
    {
        public static LibraryBranch Seed()
        {
            Librarian manager = new Librarian(
                id : "LIB-01", name: "Sara Ahmed", phone: "01012345678",
                salary : 8500m,
               hireDate:new DateOnly(2019, 3, 15));

            LibraryBranch branch = new(
                "BR-01", "City Library – Nasr City Branch", "15 Nasr Road, Nasr City, Cairo",
                "01012345678", "Sat–Thu: 09:00 AM – 09:00 PM", manager);

            branch.RegisterMember("Ahmed Kamal",
                new DateOnly(1998, 5, 10), "ahmed@email.com",
                "01098765432", new DateOnly(2023, 1, 20));

            branch.RegisterMember("Nour Hassan",
                new DateOnly(2001, 8, 22), "nour@email.com",
                "01155556677", new DateOnly(2024, 3, 5));

            branch.RegisterMember("Omar Samir", "01234567890");

            Book b1 = new("978-0-13-468599-1", "Clean Code",
                "Robert C. Martin", "Software Engineering", 2008);

            Book b2 = new("978-0-13-235088-4", "The Pragmatic Programmer",
                "David Thomas", "Software Engineering", 1999);

            Book b3 = new("978-0-06-112008-4", "To Kill a Mockingbird");

            BookCopy c1 = new("COPY-001", "Good", CopyStatus.Available, b1);
            BookCopy c2 = new("COPY-002", "Fair", CopyStatus.Available, b1);
            BookCopy c3 = new("COPY-003", "Excellent", CopyStatus.Available, b2);
            BookCopy c4 = new("COPY-004", "Good", CopyStatus.Available, b3);

            branch.AddBookCopy(c1);
            branch.AddBookCopy(c2);
            branch.AddBookCopy(c3);
            branch.AddBookCopy(c4);

            return branch;
        }
    }
}