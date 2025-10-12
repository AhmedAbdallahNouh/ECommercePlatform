namespace ECommerce.Shared.Contracts.Products
{
    public class ProductCreatedEvent
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string CategoryName { get; set; } = default!;
        public int CategoryId { get; set; } = default!;

    }

}
