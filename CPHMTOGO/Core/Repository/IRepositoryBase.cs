using System.Linq.Expressions;

namespace Core.Repository;

public interface IRepositoryBase<T>
{
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<T?> GetById(Guid id);
    T Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
    void SaveChanges();

}