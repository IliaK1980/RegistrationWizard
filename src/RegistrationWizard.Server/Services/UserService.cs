using System.Security.Cryptography;
using RegistrationWizard.Server.DataAccess;
using RegistrationWizard.Server.DataAccess.Models;
using RegistrationWizard.Server.Dtos;

namespace RegistrationWizard.Server.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task RegisterUser(RegisterUserDto user)
    {
        var userEntity = new User
        {
            Email = user.Email,
            CountryId = user.CountryId,
            ProvinceId = user.ProvinceId
        };
        
        var salt = GenerateSalt();
        userEntity.Password = HashPassword(user.Password, salt);
        userEntity.Salt = Convert.ToBase64String(salt);
        
        await userRepository.AddUser(userEntity);
    }
    
    private static byte[] GenerateSalt(int size = 32)
    {
        var salt = new byte[size];
        using var rng =RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private static string HashPassword(string password, byte[] salt)
    {
        using var rfc2898 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var hash = rfc2898.GetBytes(32);
        return Convert.ToBase64String(hash);
    }
}