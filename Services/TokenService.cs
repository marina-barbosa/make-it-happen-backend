using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace make_it_happen.Services;

public class TokenService : ITokenService
{
  public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
  {
    var key = _config.GetSection("JWT").GetValue<String>("SecretKey") ??
    throw new InvalidOperationException("Missing or invalid Secret Key");
    var privateKey = Encoding.UTF8.GetBytes(key);
    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT").GetValue<int>("ExpirationInMinutes")),
      Audience = _config.GetSection("JWT").GetValue<String>("ValidAudience") ??
      throw new InvalidOperationException("Missing or invalid Audience"),
      Issuer = _config.GetSection("JWT").GetValue<String>("ValidIssuer") ??
      throw new InvalidOperationException("Missing or invalid Issuer"),
      SigningCredentials = signinCredentials,
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
    return token;
  }

  public string GenerateRefreshToken()
  {
    var secureRandomBytes = new byte[128];
    using var randomNumberGenerator = RandomNumberGenerator.Create();
    randomNumberGenerator.GetBytes(secureRandomBytes);
    var refreshToken = Convert.ToBase64String(secureRandomBytes);
    return refreshToken;
  }

  public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
  {
    var secretKey = _config["JWT:SecretKey"] ?? throw new InvalidOperationException("Missing or invalid Secret Key");
    var tokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false,
      ValidateIssuer = false,
      ValidateIssuerSigningKey = true,
      ValidateLifetime = false,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
    if (securityToken is not JwtSecurityToken jwtSecurityToken ||
        !jwtSecurityToken.Header.Alg.Equals(
          SecurityAlgorithms.HmacSha256,
          StringComparison.InvariantCultureIgnoreCase))
    {
      throw new SecurityTokenException("Invalid token");
    }
    return principal;
  }
}