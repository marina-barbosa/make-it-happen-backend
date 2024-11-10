using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace make_it_happen.Models;
public class Campaign
{
  [Key]
  public int CampaignId { get; set; }

  public int UserId { get; set; }

  [MaxLength(80)]
  public string Name { get; set; }

  public string? Description { get; set; }

  [Column(TypeName = "decimal(18,2)")]
  public decimal? Goal { get; set; }

  [Column(TypeName = "decimal(18,2)")]
  public decimal? AmountRaised { get; set; }

  public int? CategoryId { get; set; }

  [MaxLength(80)]
  public string? Mode { get; set; }

  public DateTime? Deadline { get; set; }

  [MaxLength(500)]
  public string? ImageUrl { get; set; }

  [MaxLength(255)]
  public string? VideoUrl { get; set; }

  public string? TermsConditions { get; set; }

  public DateTime? CreationDate { get; set; }

  [MaxLength(80)]
  public string? Status { get; set; }

  [ForeignKey("UserId")]
  public virtual User? User { get; set; }

  [ForeignKey("CategoryId")]
  public virtual Category? Category { get; set; }

  public ICollection<DonateHistory>? DonationHistory { get; set; } = new List<DonateHistory>();

}