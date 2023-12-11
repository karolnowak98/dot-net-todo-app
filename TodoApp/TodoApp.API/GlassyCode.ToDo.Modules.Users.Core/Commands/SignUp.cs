using System.ComponentModel.DataAnnotations;
using GlassyCode.ToDo.Abstractions.Commands;

namespace GlassyCode.ToDo.Modules.Users.Core.Commands;

public record SignUp([Required] [EmailAddress] string Email, [Required] string Password, string Role) : ICommand
{
    public Guid UserId { get; } = Guid.NewGuid();
}