using LibraryManagementProject.Contracts;

namespace LibraryManagementProject.Entities;

// Print(IDisplayable)
public abstract class User : IDisplayable
{
    public string Name { get; protected set; } = null!;

    public string Phone { get; protected set; } = null!;

    protected User(string name, string phone)
    {
        Name = name;
        Phone = phone;
    }

    public abstract string DisplayInformations();
}
