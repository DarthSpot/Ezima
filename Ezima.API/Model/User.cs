namespace Ezima.API.Model;

public class User
{
    public int Id { get; set; }
    public string OAuthId { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public required string Username { get; set; }
    public required string EMail { get; set; }
    public required string FullName { get; set; }
    public virtual List<Child> Children { get; set; } = [];
}