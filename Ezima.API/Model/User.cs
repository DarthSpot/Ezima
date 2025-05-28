namespace Ezima.API.Model;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string EMail { get; set; }
    public required string FullName { get; set; }
    public virtual string PasswordHash { get; set; }
    public virtual string PasswordSalt { get; set; }
    public virtual List<Child> Children { get; set; } = [];
}