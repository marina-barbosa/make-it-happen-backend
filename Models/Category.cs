using System.ComponentModel.DataAnnotations;

namespace make_it_happen.Models;
public class Category
{
  [Key]
  public int CategoryId { get; set; }

  [Required]
  [MaxLength(80)]
  public string Name { get; set; }

  public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
}