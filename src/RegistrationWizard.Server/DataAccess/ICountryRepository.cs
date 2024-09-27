using RegistrationWizard.Server.DataAccess.Models;

namespace RegistrationWizard.Server.DataAccess;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllCountries();
    Task<IEnumerable<Province>> GetProvincesByCountry(int countryId);
}