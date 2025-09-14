using Microsoft.EntityFrameworkCore;

namespace ContosoService.Features.Items;

public interface IItemRepository
{
  Task<Item?> FindByIdAsync(int id);
  Task<Item?> FindBySkuAsync(string sku);
  Task<List<Item>> GetAllAsync();
  Task<List<Item>> GetVariantsAsync(int parentId);
  void Create(Item item);
  void Update(Item item);
}

public class ItemRepository(AppDbContext context) : IItemRepository
{
    public async Task<Item?> FindByIdAsync(int id) => await context.Items.FindAsync(id);

    public async Task<Item?> FindBySkuAsync(string sku) => await context.Items.FirstOrDefaultAsync(i => i.Sku == sku);

    public async Task<List<Item>> GetAllAsync() => await context.Items.ToListAsync();

    public async Task<List<Item>> GetVariantsAsync(int parentId) => await context.Items.Where(i => i.ParentId == parentId).ToListAsync();

    public void Create(Item item) => context.Items.Add(item);

    public void Update(Item item) => context.Items.Update(item);

}