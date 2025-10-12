using ECommerce.Application.Common.Interfaces.RabbitMQ;
using ECommerce.Shared.Contracts.Products;
using System.Net.Http.Json;

namespace ECommerce.Application.Common.Services.RabbitMQ
{
    public class RabbitMqPublisherService : IRabbitMqPublisherService
    {
        private readonly HttpClient _httpClient;

        public RabbitMqPublisherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PublishProductCreatedEventAsync(ProductCreatedEvent productEvent)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Publish", productEvent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to publish event: {response.StatusCode}");
            }
        }
    }

}
