using RegistrationWizard.Server.Dtos;

namespace RegistrationWizard.Server.Services;

public interface IUserService
{
    Task RegisterUser(RegisterUserDto user);
}