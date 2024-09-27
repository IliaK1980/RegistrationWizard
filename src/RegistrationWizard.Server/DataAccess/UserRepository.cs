using RegistrationWizard.Server.DataAccess.Models;

namespace RegistrationWizard.Server.DataAccess;

public class UserRepository(RegistrationWizardDbContext dbContext) : IUserRepository
{
    public async Task AddUser(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}