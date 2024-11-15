using System.ComponentModel.DataAnnotations;

namespace make_it_happen.Models
{
  public class UpdateUserDto
  {
    public int UserId { get; set; }
    [Required]
    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    public string? Bio { get; set; }

    [MaxLength(80)]
    public string? Contact { get; set; }

    [MaxLength(80)]
    public string? Status { get; set; }

    public bool? EmailVerified { get; set; }
  }
}
