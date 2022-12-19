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
    

    [HttpPut("startorder")]
    public async Task<OrderStatusDto> StartOrder(Guid OrderId, Guid EmployeeId)
    {
        return await _baseService.StartOrder(OrderId,EmployeeId);
    }

    [HttpPut("closeorder")]
    public async Task<OrderStatusDto> CloseOrder(Guid orderid)
    {
        return await _baseService.CloseOrder(orderid);
    }

    [HttpDelete("{id}")]
    public Task<bool> Delete( Guid id)
    {
        return DeleteAsync(id);
    }


}