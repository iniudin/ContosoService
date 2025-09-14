using Microsoft.EntityFrameworkCore;

namespace ContosoService.Features.Items;

public interface IItemRepository
{
  Task<Item?> FindByIdAsync(int id);
  Task<Item?> FindBySkuAsync(string sku);
  Task<List<Item>> GetAllAsync();
  void Create(Item item);
  void Update(Item item);
}

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context) => _context = context;

    public async Task<Item?> FindByIdAsync(int id) => await _context.Items.FindAsync(id);

    public async Task<Item?> FindBySkuAsync(string sku) => await _context.Items.FirstOrDefaultAsync(i => i.Sku == sku);

    public async Task<List<Item>> GetAllAsync() => await _context.Items.ToListAsync();

    public void Create(Item item) => _context.Items.Add(item);

    public void Update(Item item) => _context.Items.Update(item);


}