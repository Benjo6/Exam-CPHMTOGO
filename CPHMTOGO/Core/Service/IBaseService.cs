namespace Core.Service;



public interface IBaseService<TEntity,TDto>
{
    Task<TDto> AddAsync(TDto writeDto);
    Task<TDto> UpdateAsync(Guid id, TDto writeDto);
    Task<bool> DeleteAsync(Guid id);

    Task<TDto> GetByIdAsync(Guid id);
    Task<List<TDto>> GetListAsync();

}