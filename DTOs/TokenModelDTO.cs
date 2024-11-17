using System.Text.Json.Serialization;

namespace make_it_happen.DTOs
{
  public class TokenModelDTO
  {
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }

    // [JsonPropertyName("expiration")]
    // public DateTime Expiration { get; set; }
  }
}