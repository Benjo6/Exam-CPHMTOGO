using Core.Repository;
using OrderService.Domain;

namespace OrderService.Repositories.Interfaces;

public interface IOrderStatusRepository : IAsyncRepository<OrderStatus>
{
}