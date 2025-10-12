using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Products.DTOs;
using ECommerce.Domain.Models;
using ECommerce.Domain.Shared;
using Meilisearch;

namespace ECommerce.Infrastructure.Search
{
    public class MeiliSearchService : ISearchService
    {
        private readonly MeilisearchClient _client;
        private readonly string _indexName = "products";

        public MeiliSearchService(string hostUrl, string apiKey)
        {
            _client = new MeilisearchClient(hostUrl, apiKey);
        }

        public async Task InitializeIndexAsync()
        {
            // 🧹 احذف لو موجود قبل كده (اختياري لكن مفيد أثناء التطوير)
            var existingIndexes = await _client.GetAllIndexesAsync();
            if (existingIndexes.Results.Any(i => i.Uid == _indexName))
                await _client.DeleteIndexAsync(_indexName);

            // ✅ أنشئ index جديد مع تحديد الـ primary key بوضوح
            await _client.CreateIndexAsync(_indexName, "id");

            // ✅ احصل عليه بعد الإنشاء
            var index = await _client.GetIndexAsync(_indexName);

            // ✅ حدّد الخصائص القابلة للتصفية والفرز
            await index.UpdateFilterableAttributesAsync(new[] { "categoryName", "price" });
            await index.UpdateSortableAttributesAsync(new[] { "price", "name" });
        }

        public async Task AddOrUpdateProductAsync(Product product)
        {
            var document = new
            {
                id = product.Id,
                name = product.Name,
                description = product.Description,
                price = product.Price,
                categoryId = product.CategoryId,
                categoryName = product.Category?.Name ?? ""
            };

            await _client.Index(_indexName).AddDocumentsAsync(new[] { document });
        }

        public async Task GetAllDocuments()
        {


            var documents = await _client.Index(_indexName).GetDocumentsAsync<ProductDto>();

        }

        public async Task DeleteProductAsync(int id)
        {
            await _client.Index(_indexName).DeleteOneDocumentAsync(id.ToString());
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string query)
        {
            var results = await _client.Index(_indexName).SearchAsync<ProductDto>(query);
            return results.Hits;
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string query, int? categoryId, decimal? minPrice, decimal? maxPrice,int pageNumber = 1, int pageSize=20 )
        {
            var index = await _client.GetIndexAsync(_indexName);

            var filterConditions = new List<string>();

            if (categoryId.HasValue)
                filterConditions.Add($"categoryId = {categoryId.Value}");
            if (minPrice.HasValue)
                filterConditions.Add($"price >= {minPrice.Value}");
            if (maxPrice.HasValue)
                filterConditions.Add($"price <= {maxPrice.Value}");

            var searchQuery = new SearchQuery
            {
                Q = query,
                Filter = filterConditions.Count > 0 ? string.Join(" AND ", filterConditions) : null,
                Limit = pageSize,
                Offset = (pageNumber - 1) * pageSize
            };

            var results = await _client.Index(_indexName).SearchAsync<ProductDto>(query,searchQuery);

            return results.Hits;
        }

    }
}
