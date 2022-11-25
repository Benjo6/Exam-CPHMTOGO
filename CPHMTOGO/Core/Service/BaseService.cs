using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Entity;
using Core.Entity.Dtos;
using Core.Repository;
using Microsoft.EntityFrameworkCore;

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
        TEntity entity = await _repository.GetById(id);

        if (entity ==null)
            return null;

        TEntityDto dto = _mapper.Map<TEntityDto>(entity);

        return dto;
        
    }

    public virtual async Task<IEnumerable<TEntityDto>> GetAll()
    {
        return _repository.GetAll().ProjectTo<TEntityDto>(_mapper.ConfigurationProvider).AsQueryable();

    }

    public virtual async Task<TEntityDto> Create(TEntityDto entityDto)
    {
        TEntity entity = _mapper.Map<TEntity>(entityDto);

         _repository.Create(entity);

         return _mapper.Map<TEntityDto>(entity);
    }

    public virtual async Task<TEntityDto> Update( TEntityDto entityDto)
    { 
        _repository.Update(_mapper.Map<TEntity>(entityDto));

        return _mapper.Map<TEntityDto>(entityDto);
    }

    public async Task<bool> Delete(Guid id)
    {
        _repository.Delete(id);
        return true;
    }
}