namespace TodoApp.API.Core.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}