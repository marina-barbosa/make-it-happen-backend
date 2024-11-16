
using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;
using make_it_happen.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace make_it_happen.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository _userRepository;
  private readonly IMapper _mapper;

  public UserController(IUserRepository userRepository, IMapper mapper)
  {
    _userRepository = userRepository;
    _mapper = mapper;
  }

  [HttpGet("list")]
  // [Authorize]
  public ActionResult<IEnumerable<UserDto>> ListUsers()
  {
    var users = _userRepository.ListUsers();
    var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
    return Ok(userDtos);
  }

  [HttpGet("{id:int:min(1)}", Name = "GetUserById")]
  public ActionResult<UserProfileDto> GetUserById(int id)
  {
    var user = _userRepository.GetUserById(id);
    if (user == null) return NotFound("Usuário não encontrado");

    var userProfileDto = _mapper.Map<UserProfileDto>(user);
    return Ok(userProfileDto);
  }

  [HttpPost]
  public ActionResult<UserDto> CreateUser(CreateUserDto createUserDto)
  {
    var user = _mapper.Map<User>(createUserDto);
    _userRepository.CreateUser(user);

    var userDto = _mapper.Map<UserDto>(user);
    return CreatedAtRoute("GetUserById", new { id = user.UserId }, userDto);
  }

  [HttpPut("{id:int}")]
  public ActionResult<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
  {
    if (id != updateUserDto.UserId) return BadRequest("O id informado não pode ser diferente do id do usuário");

    var userToUpdate = _userRepository.GetUserById(id);
    if (userToUpdate == null) return NotFound("Usuário não encontrado");

    _mapper.Map(updateUserDto, userToUpdate);
    _userRepository.UpdateUser(userToUpdate);

    var userDto = _mapper.Map<UserDto>(userToUpdate);
    return Ok(userDto);
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
