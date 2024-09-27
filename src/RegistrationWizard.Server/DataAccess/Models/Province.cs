using System.ComponentModel.DataAnnotations;

namespace RegistrationWizard.Server.DataAccess.Models;

public class Province
{
  [Required]
  public int Id { get; init; }

  [Required]
  [MaxLength(100)]
  public string Name { get; init; } = null!;
    
  [Required]
  public int CountryId { get; init; }

  public Country Country { get; init; } = null!;
}