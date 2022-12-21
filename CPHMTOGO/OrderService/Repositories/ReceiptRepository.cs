using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class ReceiptRepository : RepositoryBase<Receipt>, IReceiptRepository
{
    public ReceiptRepository(RepositoryContext dbContext,ILogger<ReceiptRepository> logger) : base(dbContext,logger)
    {
    }

}