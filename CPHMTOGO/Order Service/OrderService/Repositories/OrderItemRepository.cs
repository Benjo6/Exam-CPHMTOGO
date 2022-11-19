using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderItemRepository : BaseAsyncRepository<OrderItem>,IOrderItemRepository

{
    public OrderItemRepository(DbContext dbContext, DbSet<OrderItem> contextCollection) : base(dbContext, contextCollection)
    {
    }
    
    protected override IQueryable<OrderItem> DefaultInclude()
        => base.DefaultInclude().Include(x => x.Order);

}


