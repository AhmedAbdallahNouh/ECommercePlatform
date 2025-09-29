using ECommerce.Domain.Models;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.SeedData
{
    public static class DbInitializer
    {
        public static void Seed(ECommerceDbContext context)
        {
            // لو الداتا موجودة خلاص ما نعملش حاجة
            if (context.Categories.Any())
                return;

            // Categories
            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothes" },
                new Category { Name = "Books" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Products
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 1200, Stock = 10, CategoryId = categories[0].Id },
                new Product { Name = "Smartphone", Price = 800, Stock = 15, CategoryId = categories[0].Id },
                new Product { Name = "T-Shirt", Price = 20, Stock = 50, CategoryId = categories[1].Id },
                new Product { Name = "Novel", Price = 15, Stock = 100, CategoryId = categories[2].Id }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
