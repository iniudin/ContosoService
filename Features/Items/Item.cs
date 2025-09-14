
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoService.Features.Items;

[Table("items")]
public class Item
{
  public int Id { get; set; }
  public string Sku { get; set; } = default!;
  public string? Name { get; set; }
  public decimal? Price { get; set; }

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool? IsDeleted { get; set; }

}
