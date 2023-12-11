using System.ComponentModel.DataAnnotations;
using GlassyCode.ToDo.Abstractions.Commands;

namespace GlassyCode.ToDo.Modules.Users.Core.Commands;

public record SignIn([Required] [EmailAddress] string Email, [Required] string Password) : ICommand
{
    public Guid Id { get; } = Guid.NewGuid();
}