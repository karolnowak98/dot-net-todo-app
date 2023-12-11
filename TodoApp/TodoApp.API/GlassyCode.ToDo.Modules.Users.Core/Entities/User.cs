namespace GlassyCode.ToDo.Modules.Users.Core.Entities;

internal class User
{
    public Guid Id { get; set; }
    //TODO Make value object for email
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public string RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
}