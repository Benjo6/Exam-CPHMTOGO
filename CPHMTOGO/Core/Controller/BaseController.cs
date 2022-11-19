
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controller;

[ApiController]
public class BaseController<TEntity,TDto> : ControllerBase
{
    protected readonly IBaseService<TEntity,TDto> _baseService;

    public BaseController(IBaseService<TEntity,TDto> baseService)
    {
        _baseService = baseService;
    }
    
    [NonAction]
    public async Task<IActionResult> AddAsync(TDto dto)
    {
        var result = await _baseService.AddAsync(dto);
        return Ok(result);
    }    
    [NonAction]
    public async Task<IActionResult> UpdateAsync(Guid id, TDto dto)
    {
        var result = await _baseService.UpdateAsync(id, dto);
        return Ok(result);
    }
    
    [NonAction]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _baseService.DeleteAsync(id);
        return Ok(result);
    }
    
    [NonAction]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _baseService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [NonAction]
    public async Task<IActionResult> GetListAsync()
    {
        var result = await _baseService.GetListAsync();
        return Ok(result);
    }
}