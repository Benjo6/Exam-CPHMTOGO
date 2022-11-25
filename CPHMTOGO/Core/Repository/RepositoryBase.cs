using System.Linq.Expressions;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public class RepositoryBase<T> :IRepositoryBase<T> where T:BaseEntity
{
   private readonly DbContext _context;
   private readonly DbSet<T> _dbSet;

   public RepositoryBase(DbContext context)
   {
      _context = context;
      _dbSet = _context.Set<T>();
   }


   public virtual IQueryable<T> GetAll()
   {
      return  _dbSet.AsNoTracking();
   }

   public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
   {
      return _dbSet.Where(expression).AsNoTracking();
      
   }

   public virtual async Task<T> GetById(Guid id)
   {
      return await _dbSet.AsNoTracking().Where(t => t.Id == id).FirstOrDefaultAsync();

   }

   public virtual T Create(T entity)
   {
      _dbSet.Add(entity);
      SaveChanges();
      return entity;
      
   }

   public virtual void Update(T entity)
   {
      _dbSet.Update(entity);
      SaveChanges();
   }

   public virtual void Delete(Guid id)
   {
      T exist = _dbSet.Find(id) ?? throw new InvalidOperationException();
      _dbSet.Remove(exist);
      SaveChanges();
   }

   public virtual void SaveChanges()
   {
      _context.SaveChanges();
      
   }
}