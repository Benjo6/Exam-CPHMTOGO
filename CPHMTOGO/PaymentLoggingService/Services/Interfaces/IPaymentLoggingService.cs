using Core.Service;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;

namespace PaymentLoggingService.Services.Interfaces;

public interface IPaymentLoggingService : IBaseService<PaymentLogging,PaymentLoggingDto>
{
    
}