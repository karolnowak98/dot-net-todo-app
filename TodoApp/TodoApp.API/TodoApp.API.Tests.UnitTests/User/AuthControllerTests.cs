using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Controllers;
using TodoApp.API.Core.Data;
using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Tests.UnitTests.User;

public class AuthControllerTests
{
    [Fact]
    public async Task Login_Ok_Response()
    {
        var dbContext = GetDatabaseContext();
        var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext), null, null, null, null);

        var loginDto = new LoginDto
        {
            Email = "Email",
            Password = "Password"
        };

        var authService = new Mock<IAuthService>();
        authService.Setup(service => service.LoginAsync(loginDto))
            .ReturnsAsync(new ServiceResponse<string> { Success = true });

        var controller = new AuthController(authService.Object, roleManager);

        var result = await controller.Login(loginDto);

        Assert.IsType<ActionResult<ServiceResponse<string>>>(result);
        Assert.IsAssignableFrom<OkObjectResult>(result.Result);
    }
    
    [Fact]
    public async Task Login_Unauthorized_Response()
    {
        var dbContext = GetDatabaseContext();
        var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(dbContext), null, null, null, null);

        var loginDto = new LoginDto
        {
            Email = "Email",
            Password = "Password"
        };

        var authService = new Mock<IAuthService>();
        authService.Setup(service => service.LoginAsync(loginDto))
            .ReturnsAsync(new ServiceResponse<string> { Success = false });

        var controller = new AuthController(authService.Object, roleManager);

        var result = await controller.Login(loginDto);

        Assert.IsType<ActionResult<ServiceResponse<string>>>(result);
        Assert.IsAssignableFrom<UnauthorizedObjectResult>(result.Result);
    }

    [Fact]
    public async Task SeedRoles_Tests()
    {
        var dbContext = GetDatabaseContext();
        var roleManager = new RoleManager<ApplicationRole>(
            new RoleStore<ApplicationRole>(dbContext), null, null, null, null);
        
        var controller = new AuthController(null, roleManager);
        
        var firstSeeding = await controller.SeedRoles();

        Assert.IsAssignableFrom<IActionResult>(firstSeeding);
        Assert.Equal("Role seeding done successfully!", (firstSeeding as OkObjectResult)?.Value);

        var alreadyDoneSeeding = await controller.SeedRoles();
        
        Assert.IsAssignableFrom<IActionResult>(alreadyDoneSeeding);
        Assert.Equal("Roles seeding is already done!", (alreadyDoneSeeding as OkObjectResult)?.Value);
    }
    
    private static ApplicationDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var dbContext = new ApplicationDbContext(options);
        
        return dbContext;
    }
}