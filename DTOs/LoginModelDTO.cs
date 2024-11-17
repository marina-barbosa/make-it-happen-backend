using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs
{
  public class LoginModelDTO
  {
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
  }
}