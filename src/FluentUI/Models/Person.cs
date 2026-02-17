namespace FluentUI.Models;

public class Person : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }
    public bool IsActive { get; set; }

}
