using PaymentLoggingService.Repositories;
using PaymentLoggingService.Repositories.Interfaces;

namespace PaymentLoggingService.IoC;

public static class ModelRegistry
{
    public static void AddModelRegistry(this IServiceCollection services)
    {
        services.AddScoped<IPaymentLoggingRepository, PaymentLoggingRepository>();
    }
}