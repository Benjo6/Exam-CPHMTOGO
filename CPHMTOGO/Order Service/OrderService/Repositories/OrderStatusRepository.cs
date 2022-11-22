using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
{
    public OrderStatusRepository(RepositoryContext dbContext ) : base(dbContext)
    {
    }
    
}