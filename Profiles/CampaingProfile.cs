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
    }
}
