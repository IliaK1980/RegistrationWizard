using Moq;
using RegistrationWizard.Server.DataAccess;
using RegistrationWizard.Server.DataAccess.Models;
using RegistrationWizard.Server.Services;

namespace RegistrationWizard.Server.Tests;

public class CountryServiceTests
{
    private readonly Mock<ICountryRepository> _mockCountryRepository;
    private readonly CountryService _countryService;

    public CountryServiceTests()
    {
        _mockCountryRepository = new Mock<ICountryRepository>();
        _countryService = new CountryService(_mockCountryRepository.Object);
    }

    [Fact]
    public async Task GetAllCountries_ShouldReturnListOfCountries()
    {
        // Arrange
        var countries = new List<Country>
        {
            new Country { Id = 1, Name = "Country 1" },
            new Country { Id = 2, Name = "Country 2" }
        };

        _mockCountryRepository.Setup(repo => repo.GetAllCountries())
            .ReturnsAsync(countries);

        // Act
        var result = await _countryService.GetAllCountries();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());  // Ensure two countries are returned
        Assert.Contains(result, c => c.Name == "Country 1");
        Assert.Contains(result, c => c.Name == "Country 2");
        _mockCountryRepository.Verify(repo => repo.GetAllCountries(), Times.Once);  // Ensure repository method is called
    }

    [Fact]
    public async Task GetProvincesByCountry_ShouldReturnListOfProvinces_ForValidCountryId()
    {
        // Arrange
        var countryId = 1;
        var provinces = new List<Province>
        {
            new Province { Id = 1, Name = "Province 1.1", CountryId = countryId },
            new Province { Id = 2, Name = "Province 1.2", CountryId = countryId }
        };

        _mockCountryRepository.Setup(repo => repo.GetProvincesByCountry(countryId))
            .ReturnsAsync(provinces);

        // Act
        var result = await _countryService.GetProvincesByCountry(countryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Name == "Province 1.1");
        Assert.Contains(result, p => p.Name == "Province 1.2");
        _mockCountryRepository.Verify(repo => repo.GetProvincesByCountry(countryId), Times.Once);
    }

    [Fact]
    public async Task GetProvincesByCountry_ShouldReturnEmptyList_ForInvalidCountryId()
    {
        // Arrange
        var invalidCountryId = 999;
        _mockCountryRepository.Setup(repo => repo.GetProvincesByCountry(invalidCountryId))
            .ReturnsAsync(new List<Province>());  // Return an empty list for an invalid country ID

        // Act
        var result = await _countryService.GetProvincesByCountry(invalidCountryId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);  // Ensure the result is an empty list
        _mockCountryRepository.Verify(repo => repo.GetProvincesByCountry(invalidCountryId), Times.Once);
    }
}
