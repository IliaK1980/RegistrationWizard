using System.ComponentModel.DataAnnotations;

namespace RegistrationWizard.Server.DataAccess.Models;

public class User
{
  [Required]
  public int Id { get; init; }
  
  [Required]
  [MaxLength(100)]
  public string Email { get; init; } = null!;
  
  [Required]
  [MaxLength(50)]
  public string Password { get; set; } = null!;
  
  [Required]
  [MaxLength(100)]
  public string Salt { get; set; } = null!;
  
  [Required]
  public int CountryId { get; init; }
  
  [Required]
  public int ProvinceId { get; init; }
}