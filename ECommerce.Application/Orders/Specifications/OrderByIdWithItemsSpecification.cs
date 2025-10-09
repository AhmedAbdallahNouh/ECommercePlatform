using ECommerce.Application.Common.Specifications;
using ECommerce.Domain.Models;


namespace ECommerce.Application.Orders.Specifications
{
    public sealed class OrderByIdWithItemsSpecification : Specification<Order>
    {
        public OrderByIdWithItemsSpecification(int id) : base(order => order.Id == id)
        {
            AddInclude(order => order.OrderItems);
        }
    }
}
