using Moq;
using RegistrationWizard.Server.DataAccess;
using RegistrationWizard.Server.DataAccess.Models;
using RegistrationWizard.Server.Dtos;
using RegistrationWizard.Server.Services;

namespace RegistrationWizard.Server.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task RegisterUser_ShouldCallAddUserWithHashedPasswordAndSalt()
    {
        // Arrange
        var userDto = new RegisterUserDto
        {
            Email = "test@example.com",
            Password = "TestPassword123",
            CountryId = 1,
            ProvinceId = 1
        };

        // Act
        await _userService.RegisterUser(userDto);

        // Assert
        _mockUserRepository.Verify(repo => repo.AddUser(It.Is<User>(user =>
            user.Email == userDto.Email &&
            user.CountryId == userDto.CountryId &&
            user.ProvinceId == userDto.ProvinceId &&
            !string.IsNullOrWhiteSpace(user.Password) &&  // Password should be hashed
            !string.IsNullOrWhiteSpace(user.Salt)  // Salt should be generated
        )), Times.Once);
    }

    [Fact]
    public async Task RegisterUser_ShouldGenerateUniqueSaltAndHash()
    {
        // Arrange
        var userDto1 = new RegisterUserDto
        {
            Email = "user1@example.com",
            Password = "TestPassword123",
            CountryId = 1,
            ProvinceId = 1
        };

        var userDto2 = new RegisterUserDto
        {
            Email = "user2@example.com",
            Password = "TestPassword123",  // Same password
            CountryId = 1,
            ProvinceId = 1
        };

        // Act
        await _userService.RegisterUser(userDto1);
        await _userService.RegisterUser(userDto2);

        // Assert
        _mockUserRepository.Verify(repo => repo.AddUser(It.Is<User>(user =>
            user.Email == userDto1.Email &&
            !string.IsNullOrWhiteSpace(user.Password) &&
            !string.IsNullOrWhiteSpace(user.Salt)
        )), Times.Once);

        _mockUserRepository.Verify(repo => repo.AddUser(It.Is<User>(user =>
            user.Email == userDto2.Email &&
            !string.IsNullOrWhiteSpace(user.Password) &&
            !string.IsNullOrWhiteSpace(user.Salt)
        )), Times.Once);

        // Ensure that the salts are different
        var user1Salt = _mockUserRepository.Invocations[0].Arguments[0] as User;
        var user2Salt = _mockUserRepository.Invocations[1].Arguments[0] as User;

        Assert.NotEqual(user1Salt.Salt, user2Salt.Salt);  // Salts should be unique
        Assert.NotEqual(user1Salt.Password, user2Salt.Password);  // Hashes should be different even with same password
    }
}