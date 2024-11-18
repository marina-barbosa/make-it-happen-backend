using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Profiles;

public class CampaignProfile : Profile
{
  public CampaignProfile()
  {
    CreateMap<Campaign, CampaignDto>();
    CreateMap<Campaign, CampaignDetailsDto>()
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
        .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.DonationHistory));
    CreateMap<CreateCampaignDto, Campaign>()
    .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Draft"));

    CreateMap<UpdateCampaignDto, Campaign>();

    CreateMap<Campaign, CampaignDetailsDto>()
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.Name))
        .ForMember(dest => dest.Donations, opt => opt.MapFrom(src => src.DonationHistory));
  }
}
