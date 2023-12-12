using Moq;
using Xunit;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;
using TodoApp.API.Services;

namespace TodoApp.API.Tests.UnitTests.User;

public class UserControllerTests
{
    private readonly Mock<IUsersRepository> _userRepositoryMock = new();
    
    [Fact]
    public async Task GetAllUsersAsync()
    {
        const string testEmail = "testEmail";
        var userService = new UsersService(_userRepositoryMock.Object);
        var users = new List<GetUserDto> { new() { Email = testEmail } };
        _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);

        var response = await userService.GetAllAsync();

        Assert.NotNull(response.Data);
        Assert.NotEmpty(response.Data);
        Assert.IsAssignableFrom<ServiceResponse<IEnumerable<GetUserDto>>>(response);
        Assert.Equal(testEmail, response.Data.FirstOrDefault()?.Email);
    }

    [Fact]
    public async Task GetUserByIdAsync_Found()
    {
        const string testEmail = "testEmail";
        var testUserId = Guid.NewGuid();
        var testUser = new GetUserDto { Email = testEmail };
        var userService = new UsersService(_userRepositoryMock.Object);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(testUserId)).ReturnsAsync(testUser);

        var response = await userService.GetByIdAsync(testUserId);

        Assert.IsAssignableFrom<ServiceResponse<GetUserDto>>(response);
        Assert.NotNull(response.Data);
        Assert.Equal(testEmail, response.Data.Email);
    }

    [Fact]
    public async Task GetUserByIdAsync_NotFound()
    {
        var testUserId = Guid.NewGuid();
        var userService = new UsersService(_userRepositoryMock.Object);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(testUserId)).ReturnsAsync(null as GetUserDto);

        var response = await userService.GetByIdAsync(testUserId);

        Assert.Null(response.Data);
        Assert.IsAssignableFrom<ServiceResponse<GetUserDto>>(response);
        Assert.Equal("User not found!!", response.Message);
        Assert.False(response.Success);
    }
}