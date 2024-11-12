using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace make_it_happen.Models;
public class User
{
  [Key]
  public int UserId { get; set; }

  [MaxLength(80)]
  public string Name { get; set; } = string.Empty;

  [EmailAddress]
  [MaxLength(255)]
  public string Email { get; set; } = string.Empty;

  [MaxLength(255)]
  [JsonIgnore]
  public string Password { get; set; } = string.Empty;

  [MaxLength(500)]
  public string? AvatarUrl { get; set; }

  public string? Bio { get; set; }

  [MaxLength(80)]
  public string? Contact { get; set; }

  public DateTime CreationDate { get; set; } = DateTime.UtcNow;

  [MaxLength(80)]
  public string? Status { get; set; }

  public bool? EmailVerified { get; set; }


  [JsonIgnore]
  public ICollection<DonateHistory>? DonationHistory { get; set; } = new List<DonateHistory>();

  [JsonIgnore]
  public ICollection<Campaign>? Campaigns { get; set; } = new List<Campaign>();

}