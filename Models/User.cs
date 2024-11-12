using System.ComponentModel.DataAnnotations;

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
  public string Password { get; set; } = string.Empty;

  [MaxLength(500)]
  public string? AvatarUrl { get; set; }

  public string? Bio { get; set; }

  [MaxLength(80)]
  public string? Contact { get; set; }

  public DateTime? CreationDate { get; set; }

  [MaxLength(80)]
  public string? Status { get; set; }

  public bool? EmailVerified { get; set; }

  public ICollection<DonateHistory>? DonationHistory { get; set; } = new List<DonateHistory>();

  public ICollection<Campaign>? Campaigns { get; set; } = new List<Campaign>();

}