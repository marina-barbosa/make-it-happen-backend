using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace make_it_happen.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    [MaxLength(80)]
    public string FullName { get; set; } = string.Empty;

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
}