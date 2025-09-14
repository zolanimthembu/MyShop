using MyShop.Server.DTOs;

namespace MyShop.Server.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(int page = 1, int pageSize = 20);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> GetProductBySkuAsync(string sku);
        Task<int> GetTotalProductsAsync();
    }
}
