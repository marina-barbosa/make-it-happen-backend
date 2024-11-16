using Microsoft.AspNetCore.Identity;

namespace make_it_happen.Models
{
  public class ApplicationUser : IdentityUser
  {
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
  }
}