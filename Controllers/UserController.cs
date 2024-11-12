
using make_it_happen.Context;
using make_it_happen.Models;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController(AppDbContext context) : ControllerBase
{

  // private readonly ILogger<UserController> _logger;
  private readonly AppDbContext _context = context;

  [HttpGet("list")]
  public ActionResult<IEnumerable<User>> ListUsers()
  {
    if (_context.Users == null)
    {
      return NotFound("Nenhum usuário encontrado");
    }
    var users = _context.Users.ToList();
    return Ok(users);
  }

  public ActionResult<IEnumerable<User>> GetUserById()
  {
    return Ok();
  }

  [HttpPost]
  public ActionResult<User> CreateUser(User user)
  {
    return Ok();
  }
}