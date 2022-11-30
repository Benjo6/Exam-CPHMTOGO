using Core.Repository;
using Microsoft.EntityFrameworkCore;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Infrastructure;
using PaymentLoggingService.Repositories.Interfaces;

namespace PaymentLoggingService.Repositories;

public class PaymentLoggingRepository : RepositoryBase<PaymentLogging>, IPaymentLoggingRepository
{
    public PaymentLoggingRepository(PaymentLoggingContext context) : base(context)
    {
    }
}