
namespace make_it_happen.DTOs;
public class CampaignAndCreatorDto
{
  public int CampaignId { get; set; }
  public string? UserId { get; set; }
  public string Name { get; set; } = string.Empty;
  public string? Description { get; set; }
  public decimal? Goal { get; set; }
  public decimal? AmountRaised { get; set; }
  public int? CategoryId { get; set; }
  public string? Mode { get; set; }
  public DateTime? Deadline { get; set; }
  public string? ImageUrl { get; set; }
  public string? Status { get; set; }
  public CreatorDto? Creator { get; set; }
}

