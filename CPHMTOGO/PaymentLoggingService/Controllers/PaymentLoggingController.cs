using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;
using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingService.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class PaymentLoggingController : BaseController<PaymentLogging,PaymentLoggingDto>
{
    public PaymentLoggingController(IPaymentLoggingService baseService) : base(baseService)
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
    public async Task<IActionResult> Post([FromBody] PaymentLoggingDto dto)
    {
        return await AddAsync(dto);
    }
}