namespace ECommerce.Application.Products.DTOs
{
    namespace ECommerce.API.Contracts.Products
    {
        public record GetAllProductsRequest(
            string? SearchTerm,
            int? CategoryId,
            int PageNumber = 1,
            int PageSize = 10
        );
    }

}
