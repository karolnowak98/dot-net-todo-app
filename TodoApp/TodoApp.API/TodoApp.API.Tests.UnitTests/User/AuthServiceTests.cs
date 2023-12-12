using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;
using TodoApp.API.Services;

namespace TodoApp.API.Tests.UnitTests.User
{
    public class AuthServiceTests
    {
        private const string SecureSecretKey = "dsadasdafwefewrwer1231251234123";
        private const string InsecureSecretKey = "dsa";
        private const string SecretKey = "JWT:Secret";

        private readonly Mock<IUsersRepository> _userRepositoryMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();

        [Fact]
        public async Task RegisterAsync_Success()
        {
            const string testEmail = "testEmail";
            const string testPassword = "testPassword";
            var registerDto = new RegisterDto { Email = testEmail, Password = testPassword };
            var authService = new AuthService(_userRepositoryMock.Object, null);

            _userRepositoryMock.Setup(repo => repo.CreateUserAsync(registerDto)).ReturnsAsync(new ServiceResponse());

            var response = await authService.RegisterAsync(registerDto);

            Assert.IsAssignableFrom<ServiceResponse>(response);
            Assert.True(response.Success);
        }

        [Theory]
        [InlineData("validId", "validEmail@test.com", "validPassword", true, true)]
        [InlineData("invalidId", "invalidEmail@test.com", "invalidPassword", true, false)]
        [InlineData("nullId", "nonexistentEmail@test.com", "somePassword", true, false)]
        [InlineData("validId", "validEmail@test.com", "somePassword", false, false)]
        public async Task LoginAsync_Credentials_ValidAndInvalidCases(
            string id, string email, string password, bool useSecureSecretKey, bool expectedResult)
        {
            var loginDto = new LoginDto { Email = email, Password = password };
            var user = (id == "validId") ? new ApplicationUser { Id = id, Email = email } : null;
            var authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
            var secret = useSecureSecretKey ? SecureSecretKey : InsecureSecretKey;

            _configurationMock.Setup(config => config[SecretKey]).Returns(secret);
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.CheckPasswordAsync(user, loginDto.Password)).ReturnsAsync(user != null);

            if (useSecureSecretKey)
            {
                var response = await authService.LoginAsync(loginDto);

                if (expectedResult)
                {
                    Assert.True(response.Success);
                    Assert.NotNull(response.Data);
                    Assert.IsType<string>(response.Data);
                }
                else
                {
                    Assert.False(response.Success);
                    Assert.Null(response.Data);
                }
            }
            else
            {
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                    async () => await authService.LoginAsync(loginDto));
            }
        }
    }
}