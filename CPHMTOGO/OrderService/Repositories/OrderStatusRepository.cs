using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Infrastructure;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderStatusRepository : RepositoryBase<OrderStatus>, IOrderStatusRepository
{
    private readonly RepositoryContext _dbContext;

    public OrderStatusRepository(RepositoryContext dbContext, ILogger<OrderStatusRepository> logger ) : base(dbContext,logger)
    {
        _dbContext = dbContext;
    }

    public override void Update(OrderStatus entity)
    {
        _dbContext.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChangesAsync();
    }
}