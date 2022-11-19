using Core.Repository;
using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderService.Repositories;

public class ReceiptRepository : BaseAsyncRepository<Receipt>, IReceiptRepository
{
    public ReceiptRepository(DbContext dbContext, DbSet<Receipt> contextCollection) : base(dbContext, contextCollection)
    {
    }

    protected override IQueryable<Receipt> DefaultInclude()
        => base.DefaultInclude().Include(x => x.Order);



}