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
public class OrderController : BaseController<Order,OrderDto>
{
    public OrderController(IOrderService baseService) : base(baseService)
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
    public async Task<IActionResult> Post([FromBody] OrderDto dto)
    {
        return await AddAsync(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] OrderDto dto)
    {
        return await UpdateAsync(id,dto);
    }

    [HttpDelete]
    public Task<IActionResult> Delete([FromRoute] Guid id)
    {
        return DeleteAsync(id);
    }
}