
using make_it_happen.Context;
using make_it_happen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    if (_context.Users == null) return NotFound("Nenhum usuário encontrado");
    var users = _context.Users.ToList();
    return Ok(users);
  }

  [HttpGet("{id:int}", Name = "GetUserById")]
  public ActionResult<User> GetUserById(int id)
  {
    var user = _context.Users!.FirstOrDefault(x => x.UserId == id);
    if (user == null) return NotFound("Usuário não encontrado");
    return Ok(user);
  }

  [HttpPost]
  public ActionResult<User> CreateUser(User user)
  {
    _context.Users!.Add(user);
    _context.SaveChanges();
    return new CreatedAtRouteResult("GetUserById", new { id = user.UserId }, user);
  }

  [HttpPut("{id:int}")]
  public ActionResult<User> UpdateUser(int id, User user)
  {
    if(id != user.UserId) return BadRequest("O id informado não pode ser diferente do id do usuário");

    var userToUpdate = _context.Users!.FirstOrDefault(x => x.UserId == id);
    if (userToUpdate == null) return NotFound("Usuário nao encontrado");

    userToUpdate.Name = user.Name;
    userToUpdate.Email = user.Email;
    userToUpdate.Password = user.Password;
    userToUpdate.AvatarUrl = user.AvatarUrl;
    userToUpdate.Bio = user.Bio;
    userToUpdate.Contact = user.Contact;
    userToUpdate.Status = user.Status;
    userToUpdate.EmailVerified = user.EmailVerified;

    _context.SaveChanges();
    return Ok(user);
  }
}