using System.Text.Json.Serialization;

namespace make_it_happen.DTOs
{
  public class TokenModelDTO
  {
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    // public DateTime Expiration { get; set; }
  }
}