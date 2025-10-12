using ECommerce.Shared.Contracts.Products;

namespace ECommerce.Application.Common.Interfaces.RabbitMQ
{
    public interface IRabbitMqPublisherService
    {
       Task PublishProductCreatedEventAsync(ProductCreatedEvent productEvent);

    }
}
