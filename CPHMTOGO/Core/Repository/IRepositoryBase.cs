using System.Linq.Expressions;

namespace Core.Repository;

public interface IRepositoryBase<T>
{
    IEnumerable<T> GetAll();
    IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
    T GetById(Guid id);
    T Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
    void SaveChanges();

}