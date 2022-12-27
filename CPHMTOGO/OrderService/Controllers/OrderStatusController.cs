using Core.Controller;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Domain.Model;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class OrderStatusController : BaseController<OrderStatus, OrderStatusDto>
{
    private readonly IOrderStatusService _baseService;

    public OrderStatusController(IOrderStatusService baseService,ILogger<OrderStatusController> logger) : base(baseService,logger)
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
    public async Task<OrderStatusDto> StartOrder([FromBody] StartOrderStatusModel model)
    {
        return await _baseService.StartOrder(model);
    }

    [HttpPut("closeorder")]
    public async Task<OrderStatusDto> CloseOrder([FromBody]Guid orderId)
    {
        return await _baseService.CloseOrder(orderId);
    }

    [HttpDelete("{id}")]
    public Task<IActionResult> Delete( Guid id)
    {
        return DeleteAsync(id);
    }


}