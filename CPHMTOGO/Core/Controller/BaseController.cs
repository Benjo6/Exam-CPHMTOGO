using AutoMapper;
using Core.Entity;
using Core.Entity.Dtos;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controller;

[Route("api/[Controller]")]
public class BaseController<TEntity,TEntityDto> : ControllerBase
    where TEntity:class, IBaseEntity
    where TEntityDto:class,IBaseEntityDto
{
    protected readonly IBaseService<TEntity, TEntityDto> BaseService;

    public BaseController(IBaseService<TEntity, TEntityDto> baseService)
    {
        BaseService = baseService;

    }

    [NonAction]
    public virtual async Task<IActionResult> GetListAsync()
    {
        var result = await BaseService.GetAll();
        return Ok(result);
    }
    
    [NonAction]
    public virtual async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await BaseService.GetById(id);
        return Ok(result);
    }
    
    [NonAction]
    public virtual async Task<IActionResult> AddAsync(TEntityDto entity)
    {
        var result = await BaseService.Create(entity);
        return Ok(result);
    }
    
    [NonAction]
    public virtual async Task<IActionResult> UpdateAsync(Guid id,TEntityDto entity)
    {
        var result = await BaseService.Update(id,entity);
        return Ok(result);
    }

    [NonAction]
    public virtual Task<IActionResult> DeleteAsync(Guid id)
    {
        BaseService.Delete(id);
        var result = BaseService.GetById(id);
        return Task.FromResult<IActionResult>(Ok(result.IsFaulted));
    }
    
}