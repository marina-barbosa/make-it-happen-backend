using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using make_it_happen.DTOs;
using make_it_happen.Services;
using make_it_happen.Models;
using Microsoft.AspNetCore.Authorization;

namespace make_it_happen.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ITokenService tokenService,
                      UserManager<ApplicationUser> userManager,
                      RoleManager<IdentityRole> roleManager,
                      IConfiguration configuration) : ControllerBase
{
  private readonly ITokenService _tokenService = tokenService;
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;
  private readonly IConfiguration _configuration = configuration;

  // teste token
  [HttpGet("test")]
  [Authorize]
  public async Task<IActionResult> Test()
  {
    return Ok();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginModelDTO dto)
  {
    var user = await _userManager.FindByNameAsync(dto.Username!);
    if (user is not null && await _userManager.CheckPasswordAsync(user, dto.Password!))
    {
      var userRoles = await _userManager.GetRolesAsync(user);
      var authClaims = new List<Claim>
      {
        new(ClaimTypes.Name, user.UserName!),
        new(ClaimTypes.NameIdentifier, user.Id),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };
      foreach (var userRole in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
      }
      var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
      var refreshToken = _tokenService.GenerateRefreshToken();
      _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
                                    out int refreshTokenValidityInMinutes);

      user.RefreshTokenExpiryTime =
            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

      user.RefreshToken = refreshToken;

      await _userManager.UpdateAsync(user);

      return Ok(new
      {
        token = new JwtSecurityTokenHandler().WriteToken(token),
        refreshToken,
        expiration = token.ValidTo
      });
    }
    return Unauthorized();
  }


  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterModelDTO dto)
  {
    var userExists = await _userManager.FindByNameAsync(dto.Username!);

    if (userExists != null)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,
      new ResponseDTO { Status = "Error", Message = "User already exists!" });
    }

    ApplicationUser user = new()
    {
      Email = dto.Email,
      SecurityStamp = Guid.NewGuid().ToString(),
      UserName = dto.Username
    };

    var result = await _userManager.CreateAsync(user, dto.Password!);

    if (!result.Succeeded)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,
      new ResponseDTO { Status = "Error", Message = "User creation failed!" });
    }
    return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
  }


  [HttpPost("refresh-token")]
  public async Task<IActionResult> RefreshToken([FromBody] TokenModelDTO dto)
  {
    if (dto is null)
    {
      return BadRequest("Invalid client request");
    }
    string? accessToken = dto.AccessToken
      ?? throw new ArgumentNullException(nameof(dto.AccessToken));
    string? refreshToken = dto.RefreshToken
      ?? throw new ArgumentNullException(nameof(dto.RefreshToken));
    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
    if (principal is null)
    {
      return BadRequest("Invalid access token or refresh token");
    }
    string? username = principal.Identity?.Name;
#pragma warning disable CS8604 // Possível argumento de referência nula.
    var user = await _userManager.FindByNameAsync(username);
#pragma warning restore CS8604 // Possível argumento de referência nula.
    if (user is null ||
       user.RefreshToken != refreshToken ||
       user.RefreshTokenExpiryTime <= DateTime.Now)
    {
      return BadRequest("Invalid access token or refresh token");
    }
    var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims, _configuration);
    var newRefreshToken = _tokenService.GenerateRefreshToken();
    user.RefreshToken = newRefreshToken;
    await _userManager.UpdateAsync(user);
    return new ObjectResult(new
    {
      accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
      refreshToken = newRefreshToken
    });
  }


  [HttpPost("revoke-token")]
  public async Task<IActionResult> RevokeToken(string username)
  {
    var user = await _userManager.FindByNameAsync(username);
    if (user is null) return BadRequest("Invalid client request");

    user.RefreshToken = null;
    await _userManager.UpdateAsync(user);
    return NoContent();
  }

}
