using System.ComponentModel.DataAnnotations;

namespace RegistrationWizard.Server.DataAccess.Models;

public class Country
{
  [Required]
  public int Id { get; init; }
    
  [Required]
  [MaxLength(100)]
  public string Name { get; init; } = null!;
    
  public ICollection<Province> Provinces { get; } = [];
}