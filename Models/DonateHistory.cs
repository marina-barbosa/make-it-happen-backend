using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace make_it_happen.Models;
public class DonateHistory
{
  [Key]
  public int DonateHistoryId { get; set; }

  public string? UserId { get; set; }

  public int? CampaignId { get; set; }

  [Column(TypeName = "decimal(18,2)")]
  public decimal? Amount { get; set; }

  public DateTime? DonationDate { get; set; }

  [MaxLength(80)]
  public string? Status { get; set; }

  [MaxLength(80)]
  public string? PaymentMethod { get; set; }

  public bool? ReceiptSent { get; set; }

  [ForeignKey("UserId")]
  [JsonIgnore]
  public virtual ApplicationUser? User { get; set; }

  [ForeignKey("CampaignId")]
  [JsonIgnore]
  public virtual Campaign? Campaign { get; set; }
}