using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderItemRepository : RepositoryBase<OrderItem>,IOrderItemRepository
{
    public OrderItemRepository(RepositoryContext dbContext ) : base(dbContext)
    {
    }
    
}


