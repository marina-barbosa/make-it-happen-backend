using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs
{
  public class RegisterModelDTO
  {
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
  }
}