using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository;

public class RepositoryBase<T> :IRepositoryBase<T> where T:class
{
   private readonly DbContext _context;
   private readonly DbSet<T> _dbSet;

   public RepositoryBase(DbContext context)
   {
      _context = context;
      _dbSet = _context.Set<T>();
   }


   public virtual IEnumerable<T> GetAll()
   {
      return  _dbSet.AsNoTracking().AsEnumerable();
   }

   public virtual IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
   {
      return _dbSet.Where(expression).AsNoTracking().AsEnumerable();
      
   }

   public virtual T GetById(Guid id)
   {
      return _dbSet.Find(id);
      
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
      var exist = _dbSet.Find(id);
      _dbSet.Remove(exist);
      SaveChanges();
   }

   public virtual void SaveChanges()
   {
      _context.SaveChanges();
      
   }
}