using AutoMapper;
using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderService.Controllers;

[Route("api/[controller]")]
public class ReceiptController : BaseController<Receipt,ReceiptDto>
{ 
    public ReceiptController(IReceiptService baseService, ILogger<ReceiptController> logger) : base(baseService,logger)
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
    public async Task<IActionResult> Post([FromBody] ReceiptDto dto)
    {
        return await AddAsync(dto);
    }

    [HttpPut]
    public async Task<IActionResult> Put( ReceiptDto dto)
    {
        return await UpdateAsync(dto);
    }

    [HttpDelete("{id}")]
    public Task<IActionResult> Delete( Guid id)
    {
        return DeleteAsync(id);
    }
}