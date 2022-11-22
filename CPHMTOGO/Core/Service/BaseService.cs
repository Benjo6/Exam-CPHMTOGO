using AutoMapper;
using Core.Entity;
using Core.Entity.Dtos;
using Core.Repository;

namespace Core.Service;

public class BaseService<TEntity, TEntityDto> : IBaseService<TEntity, TEntityDto> where TEntity : class, IBaseEntity where TEntityDto : class, IBaseEntityDto
{
    protected readonly IMapper _mapper;
    protected readonly IRepositoryBase<TEntity> _repository;

    public BaseService(IRepositoryBase<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TEntityDto> GetById(Guid id)
    {
        TEntity entity =  _repository.GetById(id);

        if (entity ==null)
            return null;

        TEntityDto dto = _mapper.Map<TEntityDto>(entity);

        return dto;
        
    }

    public virtual async Task<IEnumerable<TEntityDto>> GetAll()
    {
        IEnumerable<TEntity> list =  _repository.GetAll();
        return _mapper.Map<IEnumerable<TEntityDto>>(list);
    }

    public virtual async Task<TEntityDto> Create(TEntityDto entityDto)
    {
        TEntity entity = _mapper.Map<TEntity>(entityDto);

         _repository.Create(entity);

        entityDto = _mapper.Map<TEntityDto>(entity);

        return entityDto;
    }

    public virtual async Task<TEntityDto> Update(Guid id, TEntityDto entityDto)
    {
        TEntity entity =  _repository.GetById(id);
   
        _mapper.Map(entityDto, entity);
        _repository.Update(entity);
        entityDto = _mapper.Map(entity, entityDto);
        return entityDto;
    }

    public void Delete(Guid id)
    {
        _repository.Delete(id);
    }
}