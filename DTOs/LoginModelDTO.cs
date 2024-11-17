using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs
{
  public class LoginModelDTO
  {
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
  }
}