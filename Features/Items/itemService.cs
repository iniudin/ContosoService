namespace ContosoService.Features.Items;
using ContosoService.Core;

public interface IItemService
{
    Task<Item?> GetItem(int id);
    Task<List<Item>> GetItems();
    Task<List<Item>> GetVariants(int parentId);
    Task<Item> CreateItem(CreateItemDto item);
    Task<Item?> UpdateItem(int id, UpdateItemDto item);
    Task<bool> DeleteItem(int id);
}


public class ItemService(AppDbContext context, IItemRepository itemRepository) : IItemService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly AppDbContext _context = context;

    public async Task<Item?> GetItem(int id) => await _itemRepository.FindByIdAsync(id);
    public async Task<List<Item>> GetItems() => await _itemRepository.GetAllAsync();
    public async Task<List<Item>> GetVariants(int parentId) => await _itemRepository.GetVariantsAsync(parentId);
    public async Task<Item> CreateItem(CreateItemDto item)
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
    public async Task<Item?> UpdateItem(int id, UpdateItemDto item)
    {
        var existingItem = await _itemRepository.FindByIdAsync(id);
        if (existingItem is null) return null;

        existingItem.Name = item.Name ?? existingItem.Name;
        existingItem.Price = item.Price ?? existingItem.Price;
        existingItem.ParentId = item.ParentId ?? existingItem.ParentId;
        existingItem.UpdatedAt = DateTime.UtcNow;
        _itemRepository.Update(existingItem);
        await _context.SaveChangesAsync();

        return existingItem;
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