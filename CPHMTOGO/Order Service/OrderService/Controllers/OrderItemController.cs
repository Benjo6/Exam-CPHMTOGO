using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class OrderItemController: BaseController<OrderItem,OrderItemDto>
{
    public OrderItemController(IOrderItemService baseService) : base(baseService)
    {
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await base.GetListAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        return await base.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderItemDto orderItemDto)
    {
        return await base.AddAsync(orderItemDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] OrderItemDto orderItemDto)
    {
        return await base.UpdateAsync(id, orderItemDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return await base.DeleteAsync(id);
    }
    
}