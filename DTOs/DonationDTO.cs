namespace make_it_happen.DTOs;
public class DonationDto
{
  public int DonateHistoryId { get; set; }
  public int? UserId { get; set; }
  public int? CampaignId { get; set; }
  public decimal? Amount { get; set; }
  public DateTime? DonationDate { get; set; }
  public string? Status { get; set; }
  public string? PaymentMethod { get; set; }
}