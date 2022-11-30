using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;

namespace PaymentLoggingService.Profile;

public class PaymentLoggingProfile:AutoMapper.Profile
{
    public PaymentLoggingProfile()
    {
        CreateMap<PaymentLogging, PaymentLoggingDto>();
        CreateMap<PaymentLoggingDto, PaymentLogging>();
    }
}