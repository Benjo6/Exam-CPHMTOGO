using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingService.IoC;

public static class ServicesRegistry
{
    public static void AddServicesRegistry(this IServiceCollection services)
    {
        services.AddScoped<IPaymentLoggingService, Services.PaymentLoggingService>();
    }
    
}