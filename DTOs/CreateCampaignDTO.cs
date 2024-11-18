using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs;

public class CreateCampaignDto
{
  [Required]
  [MaxLength(80)]
  public string Name { get; set; } = string.Empty;

  public string? Description { get; set; }

  [Required]
  [Range(1.0, double.MaxValue, ErrorMessage = "Goal must be greater than zero.")]
  public decimal Goal { get; set; }

  public int? CategoryId { get; set; }

  [Required]
  [MaxLength(80)]
  public string Mode { get; set; } = string.Empty;

  [Required]
  public DateTime Deadline { get; set; }

  [MaxLength(500)]
  public string? ImageUrl { get; set; }

  [MaxLength(255)]
  public string? VideoUrl { get; set; }
}

