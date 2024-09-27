using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Server.Dtos;
using RegistrationWizard.Server.Services;

namespace RegistrationWizard.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController(ICountryService countryService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
    {
        var countries = await countryService.GetAllCountries();
        return Ok(countries);
    }

    [HttpGet("{countryId:int}/provinces")]
    public async Task<ActionResult<IEnumerable<ProvinceDto>>> GetProvinces(int countryId)
    {
        var provinces = await countryService.GetProvincesByCountry(countryId);
        return Ok(provinces);
    }
}
