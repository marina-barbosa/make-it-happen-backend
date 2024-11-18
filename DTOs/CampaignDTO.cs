
namespace make_it_happen.DTOs;
public class CampaignDto
{
  public int CampaignId { get; set; }
  public string Name { get; set; } = string.Empty;
  public decimal? Goal { get; set; }
  public decimal? AmountRaised { get; set; }
  public string? Status { get; set; }
}

