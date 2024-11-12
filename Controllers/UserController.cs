
using make_it_happen.Models;
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

        [HttpGet("list")]
        public ActionResult<IEnumerable<User>> ListUsers()
        {
            var users = _userRepository.ListUsers();
            return Ok(users);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound("Usuário não encontrado");
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userRepository.CreateUser(user);
            return CreatedAtRoute("GetUserById", new { id = user.UserId }, user);
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            if (id != user.UserId) return BadRequest("O id informado não pode ser diferente do id do usuário");

            var userToUpdate = _userRepository.GetUserById(id);
            if (userToUpdate == null) return NotFound("Usuário não encontrado");

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;
            userToUpdate.AvatarUrl = user.AvatarUrl;
            userToUpdate.Bio = user.Bio;
            userToUpdate.Contact = user.Contact;
            userToUpdate.Status = user.Status;
            userToUpdate.EmailVerified = user.EmailVerified;

            _userRepository.UpdateUser(userToUpdate);
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return NotFound("Usuário não encontrado");

            _userRepository.DeleteUser(id);
            return NoContent();
        }
    }