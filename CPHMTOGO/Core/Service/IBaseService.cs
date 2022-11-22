using Core.Entity;
using Core.Entity.Dtos;

namespace Core.Service;

public interface IBaseService<TEntity,TEntityDto> 
    where TEntity:class,IBaseEntity
    where TEntityDto:class,IBaseEntityDto
{
    Task<TEntityDto> GetById(Guid id);
    Task<IEnumerable<TEntityDto>> GetAll();
    Task<TEntityDto> Create(TEntityDto entity);
    Task<TEntityDto> Update(Guid guid, TEntityDto entity);
    void Delete(Guid id);

}