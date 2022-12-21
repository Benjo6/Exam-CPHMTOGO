using AutoMapper;
using Core.Entity;
using Core.Entity.Dtos;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Controller;

[Route("api/[Controller]")]
public class BaseController<TEntity,TEntityDto> : ControllerBase
    where TEntity:class, IBaseEntity
    where TEntityDto:class,IBaseEntityDto
{
    protected readonly IBaseService<TEntity, TEntityDto> BaseService;
    private readonly ILogger _logger;

    public BaseController(IBaseService<TEntity, TEntityDto> baseService, ILogger logger)
    {
        BaseService = baseService;
        _logger = logger;
    }

    [NonAction]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await BaseService.GetAll();
        return Ok(result);
    }
    
    [NonAction]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await BaseService.GetById(id);
        return Ok(result);
    }
    
    [NonAction]
    public async Task<IActionResult> AddAsync(TEntityDto entity)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"{nameof(AddAsync)} for ({nameof(TEntity)}) called.");
            var result = await BaseService.Create(entity);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unhandled exception occurred");
            throw;
        }
      
    }
    
    [NonAction]
    public async Task<IActionResult> UpdateAsync(TEntityDto entity)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"{nameof(UpdateAsync)} for ({nameof(TEntity)}) called.");
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
        catch (Exception e)
        {
            _logger.LogError(e, "An unhandled exception occurred");
            throw;
        }

    }

    [NonAction]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            if (_logger.IsEnabled(LogLevel.Debug))
                _logger.LogDebug($"{nameof(DeleteAsync)} for ({nameof(TEntity)}) called.");
            var result = await BaseService.Delete(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unhandled exception occurred");
            throw;
        }
        
    }
    
}