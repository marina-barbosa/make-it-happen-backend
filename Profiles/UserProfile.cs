using AutoMapper;
using make_it_happen.Models;
using make_it_happen.DTOs;

namespace make_it_happen.Profiles
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDto>().ReverseMap();
      CreateMap<UserDto, User>().ReverseMap();
    }
  }
}
