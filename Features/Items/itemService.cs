namespace ContosoService.Features.Items;

public interface IItemService
{
    Task<List<Item>> List();
    Task<Item?> Find(int id);
    Task<Item> Create(ItemDto item);
    Task<Item?> Update(int id, ItemDto item);
    Task<bool> Delete(int id);
}


public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly AppDbContext _context;
    public ItemService(AppDbContext context, IItemRepository itemRepository)
    {
        _context = context;
        _itemRepository = itemRepository;
    }

    public async Task<List<Item>> List() => await _itemRepository.GetAllAsync();
    public async Task<Item?> Find(int id) => await _itemRepository.FindByIdAsync(id);
    public async Task<Item> Create(ItemDto item)
    {
        var newItem = new Item
        {
            Name = item.Name,
            Price = item.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        _itemRepository.Create(newItem);
        await _context.SaveChangesAsync();

        return newItem;
    }
    public async Task<Item?> Update(int id, ItemDto item)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        if (existingItem is null) return null;

        var updatedItem = existingItem;
        updatedItem.Name = item.Name;
        updatedItem.Price = item.Price;
        updatedItem.UpdatedAt = DateTime.UtcNow;

        _itemRepository.Update(updatedItem);
        await _context.SaveChangesAsync();

        return updatedItem;
    }
    public async Task<bool> Delete(int id)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        if (existingItem is null) return false;

        existingItem.IsDeleted = true;
        _itemRepository.Update(existingItem);
        await _context.SaveChangesAsync();

        return true;
    }
}