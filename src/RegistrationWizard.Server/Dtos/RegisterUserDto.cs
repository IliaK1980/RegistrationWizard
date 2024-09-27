namespace RegistrationWizard.Server.Dtos;

public class RegisterUserDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public int CountryId { get; init; }
    public int ProvinceId { get; init; }
}