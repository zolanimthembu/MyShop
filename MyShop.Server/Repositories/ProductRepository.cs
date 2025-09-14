using Microsoft.EntityFrameworkCore;
using MyShop.Server.Data;
using MyShop.Server.Models;

namespace MyShop.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopDbContext _db;

        public ProductRepository(ShopDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            if (page < 1) page = 1;
            return await _db.Products
                .OrderByDescending(p => p.Featured)
                .ThenByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySKUAsync(string sku)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.SKU == sku);
        }

        public async Task<int> CountAsync()
        {
            return await _db.Products.CountAsync();
        }
    }
}
