using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.DataAccess.Models;

namespace RegistrationWizard.Server.DataAccess;

public class CountryRepository(RegistrationWizardDbContext dbContext) : ICountryRepository
{
    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        return await dbContext.Countries.ToListAsync();
    }

    public async Task<IEnumerable<Province>> GetProvincesByCountry(int countryId)
    {
        return await dbContext.Provinces.Where(p => p.CountryId == countryId).ToListAsync();
    }
}