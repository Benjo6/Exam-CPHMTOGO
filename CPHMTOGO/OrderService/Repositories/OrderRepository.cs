using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(RepositoryContext dbContext,ILogger<OrderRepository> logger) : base(dbContext,logger)
    {
    }


}