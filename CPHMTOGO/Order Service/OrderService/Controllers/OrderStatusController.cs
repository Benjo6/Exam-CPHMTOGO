using Core.Controller;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class OrderStatusController : BaseController<OrderStatus, OrderStatusDto>
{
    private readonly IOrderStatusService _baseService;

    public OrderStatusController(IOrderStatusService baseService) : base(baseService)
    {
        _baseService = baseService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return await GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return await AddAsync(new OrderStatusDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] OrderStatusDto dto)
    {
        return await UpdateAsync(id,dto);
    }

    [HttpDelete]
    public Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return DeleteAsync(id);
    }


}