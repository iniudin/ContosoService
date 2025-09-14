namespace ContosoService.Features.Items;
public class Item
{
  public int Id { get; set; }
  public string Sku { get; set; } = default!;
  public string? Name { get; set; }
  public decimal? Price { get; set; }

  public int? ParentId { get; set; }
  public Item? Parent { get; set; }
  public ICollection<Item> Variants { get; set; } = [];

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public bool? IsDeleted { get; set; }

}
