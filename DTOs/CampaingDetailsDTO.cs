
namespace make_it_happen.DTOs;
public class CampaignDetailsDto : CampaignDto
{
  public string? Description { get; set; }
  public DateTime? Deadline { get; set; }
  public string? CategoryName { get; set; }
  public IEnumerable<DonationDto>? Donations { get; set; }
}
