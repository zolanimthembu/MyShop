using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Server.Services;

namespace MyShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService service, ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Get paged list of products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (pageSize <= 0 || pageSize > 100) pageSize = 20;
            var items = await _service.GetProductsAsync(page, pageSize);
            var total = await _service.GetTotalProductsAsync();

            var result = new
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Items = items
            };

            return Ok(result);
        }

        /// <summary>
        /// Get product by id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetProductByIdAsync(id);
            if (item == null) return NotFound(new { Message = $"Product with id {id} not found." });
            return Ok(item);
        }

        /// <summary>
        /// Get product by SKU.
        /// </summary>
        [HttpGet("sku/{sku}")]
        public async Task<IActionResult> GetBySku(string sku)
        {
            var item = await _service.GetProductBySkuAsync(sku);
            if (item == null) return NotFound(new { Message = $"Product with SKU '{sku}' not found." });
            return Ok(item);
        }
    }
}
