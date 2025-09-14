using MyShop.Server.DTOs;
using MyShop.Server.Repositories;

namespace MyShop.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 20)
        {
            var products = await _repo.GetAllAsync(page, pageSize);
            return products.Select(p => new ProductDto(p));
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p == null ? null : new ProductDto(p);
        }

        public async Task<ProductDto?> GetProductBySkuAsync(string sku)
        {
            var p = await _repo.GetBySKUAsync(sku);
            return p == null ? null : new ProductDto(p);
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _repo.CountAsync();
        }
    }
}
