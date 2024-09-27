using RegistrationWizard.Server.DataAccess.Models;

namespace RegistrationWizard.Server.DataAccess;

public interface IUserRepository
{
    Task AddUser(User user);
}