using System.Text;

namespace LibraryManagementProject.Entities;

public class Member : User
{
    private static int _counter = 1;

    private readonly List<BorrowTransaction> _borrowTransactions = [];

    public string Id { get; private set; } = null!;

    public DateOnly? DataOfBirth { get; private set; }

    public string? Email { get; private set; }

    public DateOnly MembershipDate { get; private set; }


    public IReadOnlyList<BorrowTransaction> BorrowTransactions => _borrowTransactions;

    public Member(string name, string phone, DateOnly? dataOfBirth, 
        string? email, DateOnly membershipDate) : base(name, phone)
    {
        Id = $"MEM-{_counter++:D3}";
        DataOfBirth = dataOfBirth;
        Email = email;
        MembershipDate = membershipDate;
    }

    public Member(string name, string phone) 
            : this(name, phone, null, null, DateOnly.FromDateTime(DateTime.Today))
    {
      
    }

    public void AddBorrowTransaction(BorrowTransaction transaction) 
            => _borrowTransactions.Add(transaction);

    public string GetTransactionsHistory()
    {
        if (BorrowTransactions.Count == 0)
            return "No Transaction Count";

        var sp = new StringBuilder();

        foreach (var transaction in BorrowTransactions)
        {
            sp.Append(transaction.DisplayInformations());
        }

        return sp.ToString();
    }

    public override string DisplayInformations()
    => $"""
           ID : {Id}
           Name : {Name}
           Joined : {MembershipDate:dd/MM/yyyy}
           Phone : {Phone}
           Email : {Email ?? "N/A"}
           Borrows : {BorrowTransactions.Count}
       """;
}
