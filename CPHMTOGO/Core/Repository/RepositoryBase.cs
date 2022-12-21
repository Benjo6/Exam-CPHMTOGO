using System.Linq.Expressions;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Repository;

public class RepositoryBase<T> :IRepositoryBase<T> where T:BaseEntity
{
   private readonly DbContext _context;
   private readonly DbSet<T> _dbSet;
   private readonly ILogger _logger;

   public RepositoryBase(DbContext context, ILogger logger)
   {
      _context = context;
      _logger = logger;
      _dbSet = _context.Set<T>();
   }


   public virtual IQueryable<T> GetAll()
   {
      try
      {
         _logger.LogDebug($"Getting all {nameof(T)}");

         return  _dbSet.AsNoTracking();

      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }
   }

   public virtual IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
   {
      try
      {
         _logger.LogDebug($"Getting all by condition {nameof(T)}");

         return _dbSet.Where(expression).AsNoTracking();
      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }
      
   }

   public virtual async Task<T> GetById(Guid id)
   {
      try
      {
         _logger.LogDebug($"Getting {nameof(T)} with Id {string.Join(",", id)}");

         var item = await _dbSet.AsNoTracking().Where(t => t.Id == id).SingleOrDefaultAsync();
         return item ?? throw new Exception("No item");

      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }

   }

   public virtual T Create(T entity)
   {
      try
      {
         if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug($"Creating {entity.Id} {nameof(T)}");
         _dbSet.Add(entity);
         SaveChanges();
         return entity;


      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }
   }

   public virtual void Update(T entity)
   {
      try
      {
         if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug($"Updating {nameof(T)} with Id {string.Join(",", entity.Id)}");
         _dbSet.Update(entity);
         SaveChanges();
      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }
      
   }

   public virtual void Delete(Guid id)
   {
      try
      {
         if (_logger.IsEnabled(LogLevel.Debug))
            _logger.LogDebug($"Deleting {nameof(T)} with Id {string.Join(",", id)}");
         
         T exist = _dbSet.Find(id) ?? throw new InvalidOperationException("Item not found");
         _dbSet.Remove(exist);
         SaveChanges();
      }
      catch (Exception e)
      {
         _logger.LogError(e, "An unhandled exception occurred");
         throw;
      }
     
   }

   public virtual void SaveChanges()
   {
      _context.SaveChanges();
      
   }
}