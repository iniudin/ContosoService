namespace ContosoService.Features.Items;

public interface IItemService
{
    Task<Item?> GetItem(int id);
    Task<List<Item>> GetItems();
    Task<List<Item>> GetVariants(int parentId);
    Task<Item> CreateItem(ItemDto item);
    Task<Item?> UpdateItem(int id, ItemDto item);
    Task<bool> DeleteItem(int id);
}


public class ItemService(AppDbContext context, IItemRepository itemRepository) : IItemService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly AppDbContext _context = context;

    public async Task<Item?> GetItem(int id) => await _itemRepository.FindByIdAsync(id);
    public async Task<List<Item>> GetItems() => await _itemRepository.GetAllAsync();
    public async Task<List<Item>> GetVariants(int parentId) => await _itemRepository.GetVariantsAsync(parentId);
    public async Task<Item> CreateItem(ItemDto item)
    {
        var newItem = new Item
        {
            Name = item.Name,
            Price = item.Price,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ParentId = item.ParentId,
            IsDeleted = false
        };
        _itemRepository.Create(newItem);
        await _context.SaveChangesAsync();

        return newItem;
    }
    public async Task<Item?> UpdateItem(int id, ItemDto item)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        if (existingItem is null) return null;

        var updatedItem = existingItem;
        updatedItem.Name = item.Name;
        updatedItem.Price = item.Price;
        updatedItem.UpdatedAt = DateTime.UtcNow;
        updatedItem.ParentId = item.ParentId;
        _itemRepository.Update(updatedItem);
        await _context.SaveChangesAsync();

        return updatedItem;
    }
    public async Task<bool> DeleteItem(int id)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        if (existingItem is null) return false;

        existingItem.IsDeleted = true;
        _itemRepository.Update(existingItem);
        await _context.SaveChangesAsync();

        return true;
    }
}