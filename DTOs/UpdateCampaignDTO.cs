using System.ComponentModel.DataAnnotations;

namespace make_it_happen.DTOs;

public class UpdateCampaignDto
{
  [MaxLength(80)]
  public string? Name { get; set; }

  public string? Description { get; set; }

  public decimal? Goal { get; set; }

  public int? CategoryId { get; set; }

  [MaxLength(80)]
  public string? Mode { get; set; }

  public DateTime? Deadline { get; set; }

  [MaxLength(500)]
  public string? ImageUrl { get; set; }

  [MaxLength(255)]
  public string? VideoUrl { get; set; }
}
