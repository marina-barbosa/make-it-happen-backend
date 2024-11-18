using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs;

public class CreateDonationDto
{
  [Required]
  public int UserId { get; set; }

  [Required]
  public int CampaignId { get; set; }

  [Required]
  [Range(1.0, double.MaxValue, ErrorMessage = "The donation amount must be greater than zero.")]
  public decimal Amount { get; set; }

  public string? PaymentMethod { get; set; }
}