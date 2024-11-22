using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs
{
  public class RegisterModelDTO
  {
    [Required(ErrorMessage = "Full name is required")]
    public string? FullName { get; set; }
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
  }
}