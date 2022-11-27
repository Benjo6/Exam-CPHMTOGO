using OrderService.Repositories;
using OrderService.Repositories.Interfaces;

namespace OrderService.IoC;

public static class ModelRegistry
{
    public static void AddModelRegistry(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
    }
}