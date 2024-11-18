using AutoMapper;
using make_it_happen.DTOs;
using make_it_happen.Models;

namespace make_it_happen.Profiles;

public class DonationProfile : Profile
{
  public DonationProfile()
  {
    CreateMap<DonateHistory, DonationDto>();
    CreateMap<CreateDonationDto, DonateHistory>()
        .ForMember(dest => dest.DonationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Pending"));
  }
}
