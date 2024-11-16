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

      CreateMap<User, UserProfileDto>();

      CreateMap<User, UpdateUserDto>().ReverseMap();

      CreateMap<User, CreateUserDto>().ReverseMap();

      // Mapeamento de User para UserProfileDto (informações completas)
      // CreateMap<User, UserProfileDto>()
          // .ForMember(dest => dest.DonationHistory, opt => opt.MapFrom(src => src.DonationHistory))
          // .ForMember(dest => dest.Campaigns, opt => opt.MapFrom(src => src.Campaigns));

      // Mapeamento de DonateHistory para DonateHistoryDto
      // CreateMap<DonateHistory, DonateHistoryDto>();

      // // Mapeamento de Campaign para CampaignDto
      // CreateMap<Campaign, CampaignDto>();
    }
  }
}
