namespace make_it_happen.DTOs;
public class CampaignFilterDto
{
  public int? CategoryId { get; set; }
  public string? Mode { get; set; }
  public decimal? MinGoal { get; set; }
  public decimal? MaxGoal { get; set; }
}