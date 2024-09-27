using RegistrationWizard.Server.Dtos;

namespace RegistrationWizard.Server.Services;

public interface ICountryService
{
    Task<IEnumerable<CountryDto>> GetAllCountries();
    Task<IEnumerable<ProvinceDto>> GetProvincesByCountry(int countryId);
}