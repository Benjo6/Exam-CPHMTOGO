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
    

    [HttpPut("startorder/{orderid}/{employeeId}")]
    public async Task<OrderStatusDto> StartOrder(Guid orderid, Guid employeeId)
    {
        return await _baseService.StartOrder(orderid,employeeId);
    }

    [HttpPut("closeorder/{orderid}")]
    public async Task<OrderStatusDto> CloseOrder(Guid orderid)
    {
        return await _baseService.CloseOrder(orderid);
    }

    [HttpDelete]
    public Task<bool> Delete( Guid id)
    {
        return DeleteAsync(id);
    }


}