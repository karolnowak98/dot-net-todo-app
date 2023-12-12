using MediatR;
using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;

namespace TodoApp.API.UseCases.Users.Commands;

public class CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    : IRequestHandler<CreateUserCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var registerDto = request.RegisterDto;
        var user = mapper.Map<ApplicationUser>(registerDto);
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.UserName = registerDto.Email;

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(error => "# " + error.Description));

            return new ServiceResponse
            {
                Success = false,
                Message = "User creation failed because:" + Environment.NewLine + errorMessage
            };
        }

        await userManager.AddToRoleAsync(user, StaticUserRoles.USER);

        return new ServiceResponse();
    } 
}