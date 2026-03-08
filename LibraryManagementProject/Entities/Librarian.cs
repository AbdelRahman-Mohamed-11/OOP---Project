namespace LibraryManagementProject.Entities;

public class Librarian(string id, decimal salary, DateOnly hireDate,
    string name, string phone) : User(name, phone)
{
    public string Id { get; private set; } = id;

    public decimal Salary { get; private set; } = salary;

    public DateOnly HireDate { get; private set; } = hireDate;

    public override string DisplayInformations()
    => $"""
           ID : {Id}
           Name : {Name}
           Phone : {Phone}
           Salary : {Salary: C}
           Hired : {HireDate:dd/MM/yyyy}
       """;
}
