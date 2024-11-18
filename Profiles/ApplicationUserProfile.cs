using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Profiles;

public class ApplicationUserProfile : Profile
{
  public ApplicationUserProfile()
  {
    CreateMap<ApplicationUser, ApplicationUserDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
        .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => src.AvatarUrl))
        .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
        .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact))
        .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
  }
}
