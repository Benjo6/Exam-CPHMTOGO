using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderStatusRepository : BaseAsyncRepository<OrderStatus>, IOrderStatusRepository
{
    public OrderStatusRepository(DbContext dbContext, DbSet<OrderStatus> contextCollection) : base(dbContext, contextCollection)
    {
    }
    

}