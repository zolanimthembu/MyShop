using MyShop.Server.Models;

namespace MyShop.Server.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetBySKUAsync(string sku);
        Task<int> CountAsync();
    }
}
