using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class OrderController : BaseController<Order,OrderDto>
{
    public OrderController(IOrderService baseService) : base(baseService)
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
    public async Task<IActionResult> Post([FromBody] OrderDto orderDto)
    {
        return await base.AddAsync(orderDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] OrderDto orderDto)
    {
        return await base.UpdateAsync(id, orderDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return await base.DeleteAsync(id);
    }
    
    
}