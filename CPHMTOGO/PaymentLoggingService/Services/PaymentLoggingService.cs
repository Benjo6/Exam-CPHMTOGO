using AutoMapper;
using Core.Repository;
using Core.Service;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;
using PaymentLoggingService.Repositories.Interfaces;
using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingService.Services;

public class PaymentLoggingService: BaseService<PaymentLogging,PaymentLoggingDto>,IPaymentLoggingService
{
    public PaymentLoggingService(IPaymentLoggingRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}