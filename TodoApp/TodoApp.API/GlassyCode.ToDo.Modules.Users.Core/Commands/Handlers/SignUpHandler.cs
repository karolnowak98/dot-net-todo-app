using GlassyCode.ToDo.Abstractions;
using GlassyCode.ToDo.Abstractions.Commands;
using GlassyCode.ToDo.Abstractions.Messaging;
using GlassyCode.ToDo.Abstractions.Time;
using GlassyCode.ToDo.Modules.Users.Core.Entities;
using GlassyCode.ToDo.Modules.Users.Core.Events;
using GlassyCode.ToDo.Modules.Users.Core.Exceptions;
using GlassyCode.ToDo.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GlassyCode.ToDo.Modules.Users.Core.Commands.Handlers;

internal class SignUpHandler(
    IUserRepository userRepository, IRoleRepository roleRepository,
    IPasswordHasher<User> passwordHasher, IClock clock, IMessageBroker messageBroker,
    RegistrationOptions registrationOptions, ILogger<SignUpHandler> logger) : ICommandHandler<SignUp>
{
    public async Task HandleAsync(SignUp command, CancellationToken cancellationToken = default)
    {
        if (!registrationOptions.Enabled)
        {
            throw new SignUpDisabledException();
        }
            
        var email = command.Email.ToLowerInvariant();
        var provider = email.Split("@").Last();
        if (registrationOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
        {
            throw new InvalidEmailException(email);
        }

        if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length is > 100 or < 6)
        {
            throw new InvalidPasswordException();
        }
            
        var user = await userRepository.GetAsync(email);
        if (user is not null)
        {
            throw new EmailInUseException(email);
        }

        var roleName = string.IsNullOrWhiteSpace(command.Role) ? Role.Default : command.Role.ToLowerInvariant();
        var role = await roleRepository.GetAsync(roleName)
            .NotNull(() => new RoleNotFoundException(roleName));

        var now = clock.CurrentDate();
        var password = passwordHasher.HashPassword(default, command.Password);
        user = new User
        {
            Id = command.UserId,
            Email = email,
            Password = password,
            Role = role,
            CreatedAt = now
        };
        await userRepository.AddAsync(user);
        await messageBroker.PublishAsync(new SignedUp(user.Id, email, role.Name), cancellationToken);
        logger.LogInformation($"User with ID: '{user.Id}' has signed up.");
    }
}