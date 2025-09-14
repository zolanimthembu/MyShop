using MyShop.Server.Models;

namespace MyShop.Server.DTOs
{
    public class ProductDto
    {
        public ProductDto() { }

        public ProductDto(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            SKU = p.SKU;
            Price = p.Price;
            Quantity = p.Quantity;
            ImageUrl = p.ImageUrl;
            Featured = p.Featured;
            OnSpecial = p.OnSpecial;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public bool Featured { get; set; }
        public bool OnSpecial { get; set; }
    }
}
