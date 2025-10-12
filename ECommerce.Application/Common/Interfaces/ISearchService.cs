using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Models;

namespace ECommerce.Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task InitializeIndexAsync();

        Task AddOrUpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task GetAllDocuments();
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string query);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string query, int? categoryId, decimal? minPrice, decimal? maxPrice, int pageNumber = 1, int pageSize = 20);



    }
}
