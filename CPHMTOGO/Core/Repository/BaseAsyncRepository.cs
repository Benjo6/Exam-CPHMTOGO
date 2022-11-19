using Microsoft.EntityFrameworkCore;
using OrderService.Domain.EntityHelper;

namespace Core.Repository;

public abstract class BaseAsyncRepository<T> : BaseAsyncRepository<T,Guid>,IAsyncRepository<T> where T : class,EntityWithId<Guid>
{
 protected BaseAsyncRepository(DbContext dbContext, DbSet<T> contextCollection):base(dbContext,contextCollection)
 {
 }
}

public abstract class BaseAsyncRepository<T, TIdType> : IAsyncRepository<T, TIdType>
 where T : class, EntityWithId<TIdType>
 where TIdType : notnull
{
 protected readonly DbContext _context;
 protected readonly DbSet<T> _contextCollection;

 protected BaseAsyncRepository(DbContext context, DbSet<T> contextCollection)
 {
  _context = context;
  _contextCollection = contextCollection;
 }
 
 public Task<T?> GetAsync(TIdType id)
  => DefaultInclude().FirstOrDefaultAsync(x => x.Id.Equals(id));

 public IAsyncEnumerable<T> GetAllAsync() => DefaultInclude().AsAsyncEnumerable();

 public async Task<T> CreateAsync(T entity)
 {
  await _contextCollection.AddAsync(entity);
  await _context.SaveChangesAsync();
  return entity;
 }

 public async Task<T> UpdateAsync(T entity)
 {
  _contextCollection.Update(entity);
  await _context.SaveChangesAsync();
  return entity;
 }

 public async Task DeleteAsync(TIdType id)
 {
  T entity = await _contextCollection.FirstAsync(x => x.Id.Equals(id));
  _contextCollection.Remove(entity);
  await _context.SaveChangesAsync();
 }



 protected virtual IQueryable<T> DefaultInclude() => _contextCollection;


}