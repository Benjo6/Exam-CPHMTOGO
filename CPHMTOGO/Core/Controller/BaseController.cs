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
    public virtual async Task<IActionResult> UpdateAsync(TEntityDto entity)
    {
        var result =  BaseService.Update(entity);
        if (!result.IsCompletedSuccessfully)
        {
            return BadRequest(result);
        }

        if (result.Result is null)
        {
            return NoContent();
        }

        return Ok(result);

    }

    [NonAction]
    public virtual Task<bool> DeleteAsync(Guid id)
    {
        var result = BaseService.Delete(id);
        return result;
    }
    
}