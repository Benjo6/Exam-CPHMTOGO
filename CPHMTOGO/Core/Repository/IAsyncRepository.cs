namespace Core.Repository;

public interface IAsyncRepository<T> : IAsyncRepository<T, Guid>
{
}

public interface IAsyncRepository<T,TIdType>
{
    Task<T?> GetAsync(TIdType id);
    IAsyncEnumerable<T> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(TIdType id);    
}