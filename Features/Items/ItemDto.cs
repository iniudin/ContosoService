namespace ContosoService.Features.Items;

public class ItemDto
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public ItemDto() { }
    public ItemDto(Item item) => (Name, Price) = (item.Name, item.Price);
}