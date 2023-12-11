using GlassyCode.ToDo.Abstractions;
using GlassyCode.ToDo.Abstractions.Auth;
using GlassyCode.ToDo.Abstractions.Commands;
using GlassyCode.ToDo.Abstractions.Messaging;
using GlassyCode.ToDo.Modules.Users.Core.Entities;
using GlassyCode.ToDo.Modules.Users.Core.Events;
using GlassyCode.ToDo.Modules.Users.Core.Exceptions;
using GlassyCode.ToDo.Modules.Users.Core.Repositories;
using GlassyCode.ToDo.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GlassyCode.ToDo.Modules.Users.Core.Commands.Handlers;

internal class SignInHandler(
    IUserRepository userRepository, IMessageBroker messageBroker,
    IPasswordHasher<User> passwordHasher, ILogger<SignInHandler> logger, IAuthManager authManager,
    IUserRequestStorage userRequestStorage)
    : ICommandHandler<SignIn>
{
    public async Task HandleAsync(SignIn command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetAsync(command.Email.ToLowerInvariant())
            .NotNull(() => new InvalidCredentialsException());

        if (passwordHasher.VerifyHashedPassword(default, user.Password, command.Password) ==
            PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Role.Permissions
        };

        var jwt = authManager.CreateToken(user.Id, user.Role.Name, claims: claims);

        jwt.Email = user.Email;
        await messageBroker.PublishAsync(new SignedIn(user.Id), cancellationToken);
        logger.LogInformation($"User with ID: '{user.Id}' has signed in.");
        userRequestStorage.SetToken(command.Id, jwt);
    }
}