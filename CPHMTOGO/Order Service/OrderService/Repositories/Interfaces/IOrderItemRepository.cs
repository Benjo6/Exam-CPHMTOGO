using Core.Repository;
using OrderService.Domain;

namespace OrderService.Repositories.Interfaces;

public interface IOrderItemRepository : IAsyncRepository<OrderItem>
{
    
}