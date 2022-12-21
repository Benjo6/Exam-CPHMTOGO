using AutoMapper;
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
    private readonly IOrderService _baseService;

    public OrderController(IOrderService baseService, ILogger<OrderController> logger) : base(baseService,logger)
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
    public async Task<OrderDto> CreateOrder(Guid address, Guid customerId, Guid restaurantId, [FromBody] List<CreateOrderItemDto> orderDtos)
    {
       return await _baseService.CreateOrderTask(new OrderDto{Address = address,RestaurantId = restaurantId,CustomerId = customerId},orderDtos);
    }

    [HttpPut]
    public async Task<IActionResult> Put( OrderDto dto)
    {
        return await UpdateAsync(dto);
    }

    [HttpDelete]
    public Task<IActionResult> Delete( Guid id)
    {
        return DeleteAsync(id);
    }

    [HttpGet("open-order")]
    public async Task<IEnumerable<OrderDto>> GetOpenOrdersForEmployees()
    {
        return await _baseService.GetOpenOrders();

    }

    [HttpGet("number-order")]
    public async Task<int> NumberOfOpenOrders()
    {
        return await _baseService.NumberOfOpenOrders();
    }
    
  
    
    
}