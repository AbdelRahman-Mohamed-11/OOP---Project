using LibraryManagementProject.Contracts;

namespace LibraryManagementProject.Entities;

public class LibraryBranch : IDisplayable
{
    public string Id { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string Address { get; private set; } = null!;

    public string Phone { get; private set; } = null!;

    public string OpeningHours { get; private set; } = null!;

    public Librarian Manager { get; private set; } = null!;

    private readonly List<BookCopy> _copies = [];

    private readonly List<Member> _members = [];

    public LibraryBranch(string id, string name, string address, 
        string phone, string openingHours, Librarian manager)
    {
        Id = id;
        Name = name;
        Address = address;
        Phone = phone;
        OpeningHours = openingHours;
        Manager = manager;
    }

    public IReadOnlyList<Member> Members => _members;
    public IReadOnlyList<BookCopy> BookCopies => _copies;

    // Return All Users

    public IReadOnlyList<User> Users
    {
        get
        {
            List<User> users = [];

            users.Add(Manager);

            users.AddRange(Members);

            return users;
        }
    }


    // add Member 
    public Member RegisterMember(string name, DateOnly? dateOfBirth, 
        string email, string phone, DateOnly memberShipDate)
    {
        var member = new Member(name :name, phone, dateOfBirth, email, memberShipDate);
      
        _members.Add(member);

        return member;
    }

    public Member RegisterMember(string name, string phone)
    {
        var member = new Member(name: name, phone);

        _members.Add(member);

        return member;
    }

    // find Member ()

    public Member FindMemberById(string id)
    {
        //    MEM-001
        string normalized = id.NormalizeId();

        foreach (var member in Members)
        {
            if(member.Id.Equals(normalized, StringComparison.OrdinalIgnoreCase))
                return member;
        }

        throw new InvalidOperationException("Member NOT Found");
    }

    public void AddBookCopy(BookCopy bookCopy)
        => _copies.Add(bookCopy);


    public BookCopy FindCopyByCopyId(string id)
    {
        string normalized = id.NormalizeId();

        foreach (var copy in BookCopies)
        {
            if (copy.CopyId.Equals(normalized, StringComparison.OrdinalIgnoreCase))
                return copy;
        }

        throw new InvalidOperationException("Book Copy NOT Found");
    }

    // Get Available Book Copies

    public List<BookCopy> GetAvailableBookCopies()
    {
        List<BookCopy> availableBookCopies = [];

        foreach (var bookCopy in BookCopies)
        {
            if(bookCopy.IsAvailable())
                availableBookCopies.Add(bookCopy);
        }

        return availableBookCopies;
    }

    public string DisplayInformations()
    => $"""
           ID : {Id}
           Name : {Name}
           Address : {Address}
           Phone : {Phone}
           Opening Hours : {OpeningHours}
           Manager : {Manager.Name}
           Total Members : {_members.Count}
           Total Book Copies : {_copies.Count}
       """;
}
