using Core.Controller;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;
using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingService.Controllers;

[Route("api/[controller]")]
public class PaymentLoggingController : BaseController<PaymentLogging,PaymentLoggingDto>
{
    public PaymentLoggingController(IPaymentLoggingService baseService, ILogger<PaymentLoggingController> logger) : base(baseService,logger)
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