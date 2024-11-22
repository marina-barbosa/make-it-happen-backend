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
                      IConfiguration configuration,
                      ILogger<AuthController> logger) : ControllerBase
{
  private readonly ITokenService _tokenService = tokenService;
  private readonly UserManager<ApplicationUser> _userManager = userManager;
  private readonly RoleManager<IdentityRole> _roleManager = roleManager;
  private readonly IConfiguration _configuration = configuration;
  private readonly ILogger<AuthController> _logger = logger;

  [HttpGet("test")]
  [Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminOnly")]
  public IActionResult Test()
  {
    return Ok();
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginModelDTO dto)
  {
    var user = await _userManager.FindByEmailAsync(dto.Email!);
    if (user is not null && await _userManager.CheckPasswordAsync(user, dto.Password!))
    {
      var userRoles = await _userManager.GetRolesAsync(user);
      var authClaims = new List<Claim>
      {
        new(ClaimTypes.Name, user.FullName!),
        new(ClaimTypes.Email, user.Email!),
        new("id", user.UserName!),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };
      foreach (var userRole in userRoles)
      {
        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
      }
      var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
      var refreshToken = _tokenService.GenerateRefreshToken();
      _ = int.TryParse(_configuration["JWT:RefreshTokenExpirationInDays"],
                                    out int refreshTokenValidityInDays);

      user.RefreshTokenExpiryTime =
            DateTime.UtcNow.AddDays(refreshTokenValidityInDays);

      user.RefreshToken = refreshToken;

      await _userManager.UpdateAsync(user);

      return Ok(new
      {
        Token = new JwtSecurityTokenHandler().WriteToken(token),
        RefreshToken = refreshToken,
        Expiration = token.ValidTo
      });
    }
    return Unauthorized();
  }


  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterModelDTO dto)
  {
    var userEmailExists = await _userManager.FindByEmailAsync(dto.Email!);

    if (userEmailExists != null)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,
      new ResponseDTO { Status = "Error", Message = "User already exists!" });
    }

    ApplicationUser user = new()
    {
      FullName = dto.FullName!,
      Email = dto.Email,
      SecurityStamp = Guid.NewGuid().ToString(),
      UserName = dto.Username
    };

    var result = await _userManager.CreateAsync(user, dto.Password!);

    if (!result.Succeeded)
    {
      var errors = string.Join(", ", result.Errors.Select(e => e.Description));
      return StatusCode(StatusCodes.Status500InternalServerError,
      new ResponseDTO { Status = "Error", Message = $"User creation failed: {errors}" });
    }
    return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
  }


  [HttpPost("refresh-token")]
  public async Task<IActionResult> RefreshToken([FromBody] TokenModelDTO dto)
  {
    if (dto is null)
    {
      return BadRequest("Invalid client request.");
    }

    if (string.IsNullOrEmpty(dto.AccessToken) || string.IsNullOrEmpty(dto.RefreshToken))
    {
      return BadRequest("Access token and refresh token are required.");
    }

    string accessToken = dto.AccessToken;
    string refreshToken = dto.RefreshToken;

    // Obter informações do token expirado
    var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);

    if (principal is null)
    {
      return BadRequest("Invalid access token or refresh token.");
    }

    // Buscar o email no token
    string? email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

    if (string.IsNullOrEmpty(email))
    {
      return BadRequest("Email not found in the access token.");
    }

    // Buscar o usuário pelo email
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      return BadRequest("Invalid access token or refresh token!");
    }

    // Validar o Refresh Token
    if (user.RefreshToken != refreshToken)
    {
      return BadRequest("Invalid access token or refresh token!");
    }

    // Verificar se o Refresh Token está expirado
    if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
    {
      return BadRequest("Invalid access token or refresh token!");
    }

    // Gerar novos tokens
    var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
    var newRefreshToken = _tokenService.GenerateRefreshToken();

    // Atualizar o Refresh Token no banco
    user.RefreshToken = newRefreshToken;
    user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
        int.Parse(_configuration["JWT:RefreshTokenExpirationInDays"] ?? "7"));

    await _userManager.UpdateAsync(user);

    // Retornar os novos tokens
    return new ObjectResult(new
    {
      AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
      RefreshToken = newRefreshToken
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


  [HttpPost("create-role")]
  public async Task<IActionResult> CreateRole(string roleName)
  {
    var roleExists = await _roleManager.RoleExistsAsync(roleName);
    if (!roleExists)
    {
      var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

      if (roleResult.Succeeded)
      {
        _logger.LogInformation($"Role '{roleName}' created successfully.");
        return Ok(
          new ResponseDTO
          {
            Status = "Success",
            Message = $"Role '{roleName}' created successfully."
          });
      }
      else
      {
        var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
        return StatusCode(StatusCodes.Status500InternalServerError,
        new ResponseDTO { Status = "Error", Message = $"Role creation failed: {errors}" });
      }
    }

    return StatusCode(StatusCodes.Status400BadRequest,
    new ResponseDTO { Status = "Error", Message = $"Role '{roleName}' already exists." });
  }


  [HttpPost("add-role-to-user")]
  public async Task<IActionResult> AddUserToRole(string email, string roleName)
  {
    var user = await _userManager.FindByEmailAsync(email);

    if (user != null)
    {
      var result = await _userManager.AddToRoleAsync(user, roleName);
      if (result.Succeeded)
      {
        _logger.LogInformation($"User '{email}' added to role '{roleName}' successfully.");
        return Ok(
          new ResponseDTO
          {
            Status = "Success",
            Message = $"User '{email}' added to role '{roleName}' successfully."
          });
      }
      else
      {
        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return StatusCode(StatusCodes.Status500InternalServerError,
        new ResponseDTO { Status = "Error", Message = $"User '{email}' could not be added to role '{roleName}': {errors}" });
      }
    }
    return BadRequest("User not found.");
  }
}
