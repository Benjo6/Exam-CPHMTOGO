using AutoMapper;
using Core.Controller;
using Core.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class OrderItemController: BaseController<OrderItem,OrderItemDto>
{
    public OrderItemController(IOrderItemService baseService,ILogger<OrderItemController> logger) : base(baseService,logger)
    {
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
    public async Task<IActionResult> Post([FromBody] OrderItemDto dto)
    {
        return await AddAsync(dto);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrderItemDto dto)
    {
        return await UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete( Guid id)
    {
        return await DeleteAsync(id);
    }
}