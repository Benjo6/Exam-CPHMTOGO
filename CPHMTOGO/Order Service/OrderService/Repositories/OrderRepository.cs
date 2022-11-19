using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderRepository : BaseAsyncRepository<Order>, IOrderRepository
{
    public OrderRepository(DbContext dbContext, DbSet<Order> contextCollection) : base(dbContext, contextCollection)
    {
    }

    protected override IQueryable<Order> DefaultInclude()
        => base.DefaultInclude().Include(x => x.OrderStatus);

}