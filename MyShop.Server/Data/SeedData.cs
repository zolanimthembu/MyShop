using Microsoft.EntityFrameworkCore;
using MyShop.Server.Models;

namespace MyShop.Server.Data
{
    public static class SeedData
    {
        public static async Task EnsureSeedDataAsync(ShopDbContext context)
        {
            // Ensure DB created and migrations applied
            // (Migrations applied in Program.cs before calling.)
            if (await context.Products.AnyAsync())
                return;

            var products = new List<Product>
            {
                new Product { Name = "Wireless Headphones", SKU = "WH-001", Price = 129.99m, Quantity = 50, ImageUrl = "/images/wh-001.jpg", Featured = true },
                new Product { Name = "Mechanical Keyboard", SKU = "MK-002", Price = 89.50m, Quantity = 40, ImageUrl = "/images/mk-002.jpg", Featured = true },
                new Product { Name = "Gaming Mouse", SKU = "GM-003", Price = 49.99m, Quantity = 75, ImageUrl = "/images/gm-003.jpg" },
                new Product { Name = "4K Monitor", SKU = "4KM-004", Price = 349.99m, Quantity = 20, ImageUrl = "/images/4km-004.jpg", OnSpecial = true },
                new Product { Name = "USB-C Hub", SKU = "UCH-005", Price = 24.99m, Quantity = 120, ImageUrl = "/images/uch-005.jpg" },
                new Product { Name = "External SSD 1TB", SKU = "SSD-006", Price = 119.99m, Quantity = 30, ImageUrl = "/images/ssd-006.jpg", Featured = true },
                new Product { Name = "Laptop Stand", SKU = "LS-007", Price = 29.99m, Quantity = 60, ImageUrl = "/images/ls-007.jpg" },
                new Product { Name = "Webcam 1080p", SKU = "WC-008", Price = 39.99m, Quantity = 45, ImageUrl = "/images/wc-008.jpg" },
                new Product { Name = "Bluetooth Speaker", SKU = "BS-009", Price = 59.99m, Quantity = 55, ImageUrl = "/images/bs-009.jpg" },
                new Product { Name = "Smartwatch", SKU = "SW-010", Price = 199.99m, Quantity = 25, ImageUrl = "/images/sw-010.jpg", OnSpecial = true },
                new Product { Name = "Noise Cancelling Earbuds", SKU = "NE-011", Price = 79.99m, Quantity = 60, ImageUrl = "/images/ne-011.jpg" },
                new Product { Name = "Portable Charger", SKU = "PC-012", Price = 34.99m, Quantity = 100, ImageUrl = "/images/pc-012.jpg" },
                new Product { Name = "Smart Light Bulb (Pack)", SKU = "SL-013", Price = 19.99m, Quantity = 200, ImageUrl = "/images/sl-013.jpg" },
                new Product { Name = "Router (Wi-Fi 6)", SKU = "RT-014", Price = 149.99m, Quantity = 15, ImageUrl = "/images/rt-014.jpg" },
                new Product { Name = "Action Camera", SKU = "AC-015", Price = 249.99m, Quantity = 10, ImageUrl = "/images/ac-015.jpg" },
                new Product { Name = "Wireless Charger", SKU = "WC-016", Price = 19.99m, Quantity = 75, ImageUrl = "/images/wc-016.jpg" },
                new Product { Name = "Smart Doorbell", SKU = "SD-017", Price = 99.99m, Quantity = 30, ImageUrl = "/images/sd-017.jpg" },
                new Product { Name = "Fitness Tracker", SKU = "FT-018", Price = 59.99m, Quantity = 80, ImageUrl = "/images/ft-018.jpg" },
                new Product { Name = "Desk Lamp", SKU = "DL-019", Price = 22.50m, Quantity = 90, ImageUrl = "/images/dl-019.jpg" },
                new Product { Name = "HDMI Cable (2m)", SKU = "HC-020", Price = 9.99m, Quantity = 300, ImageUrl = "/images/hc-020.jpg" }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
