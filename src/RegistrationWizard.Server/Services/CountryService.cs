using RegistrationWizard.Server.DataAccess;
using RegistrationWizard.Server.Dtos;

namespace RegistrationWizard.Server.Services;

public class CountryService(ICountryRepository countryRepository) : ICountryService
{
    public async Task<IEnumerable<CountryDto>> GetAllCountries()
    {
        return (await countryRepository.GetAllCountries()).Select(x => new CountryDto
        {
            Id = x.Id,
            Name = x.Name
        });
    }

    public async Task<IEnumerable<ProvinceDto>> GetProvincesByCountry(int countryId)
    {
        return (await countryRepository.GetProvincesByCountry(countryId)).Select(x => new ProvinceDto
        {
            Id = x.Id,
            Name = x.Name
        });
    }
}