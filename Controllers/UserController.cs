using make_it_happen.DTOs;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository _userRepository;

  public UserController(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllUsers()
  {
    var users = await _userRepository.GetAllUsersAsync();
    return Ok(users.Select(u => new ApplicationUserDto
    {
      Id = u.Id,
      FullName = u.FullName,
      AvatarUrl = u.AvatarUrl,
      Bio = u.Bio,
      Contact = u.Contact,
      CreationDate = u.CreationDate,
      Status = u.Status
    }));
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(string id)
  {
    var user = await _userRepository.GetUserByIdAsync(id);
    if (user == null) return NotFound();

    return Ok(new ApplicationUserDto
    {
      Id = user.Id,
      FullName = user.FullName,
      AvatarUrl = user.AvatarUrl,
      Bio = user.Bio,
      Contact = user.Contact,
      CreationDate = user.CreationDate,
      Status = user.Status
    });
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateUser(string id, [FromBody] ApplicationUserDto userDto)
  {
    var user = await _userRepository.GetUserByIdAsync(id);
    if (user == null) return NotFound();

    user.FullName = userDto.FullName;
    user.AvatarUrl = userDto.AvatarUrl;
    user.Bio = userDto.Bio;
    user.Contact = userDto.Contact;
    user.Status = userDto.Status;

    var result = await _userRepository.UpdateUserAsync(user);
    if (!result) return BadRequest();

    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(string id)
  {
    var result = await _userRepository.DeleteUserAsync(id);
    if (!result) return NotFound();

    return NoContent();
  }
}
