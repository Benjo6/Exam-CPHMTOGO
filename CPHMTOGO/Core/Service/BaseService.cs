using AutoMapper;
using Core;
using Core.Repository;
using OrderService.Domain.EntityHelper;


namespace Core.Service;

public abstract class BaseService<TEntity,TDto> : IBaseService<TEntity,TDto>
where TEntity:class,EntityWithId<Guid>
where TDto:class,new()

{
    protected readonly IAsyncRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    protected BaseService(IAsyncRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TDto> AddAsync(TDto dto)
    {
        TEntity entity = _mapper.Map<TEntity>(dto);
        TEntity addedEntity = await _repository.CreateAsync(entity);

        if (addedEntity == null)
        {
            throw new Exception("Add is unsuccessful");
        }

        return _mapper.Map<TDto>(addedEntity);
    }

    public async Task<TDto> UpdateAsync(Guid id, TDto dto)
    {
        TEntity updatedEntity = await _repository.GetAsync(id) ?? throw new InvalidOperationException("Not Found");

        _mapper.Map<TDto>(updatedEntity);

        TEntity entity = await _repository.UpdateAsync(updatedEntity);

        if (entity==null)
        {
            throw new Exception("Update is unsuccessful");
        }

        return _mapper.Map<TDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        TEntity entity = await _repository.GetAsync(id) ?? throw new InvalidOperationException("Not Found");

        await _repository.DeleteAsync(entity.Id);

        return true;
    }

    public async Task<TDto> GetByIdAsync(Guid id)
    {
        TEntity entity = await _repository.GetAsync(id) ?? throw new InvalidOperationException("Not Found");

        TDto dto = _mapper.Map<TDto>(entity);

        return dto;
    }

    public async Task<List<TDto>> GetListAsync()
    {
        IAsyncEnumerable<TEntity> entities =  _repository.GetAllAsync();

        if (entities==null)
        {
            throw new Exception("Not Found");
        }

        List<TDto> readDtos = _mapper.Map<List<TDto>>(entities);

        return readDtos;
    }
}

