using Core.Repository;
using OrderService.Domain;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

namespace OrderService.IoC;

public static class ServicesRegistry
{
    public static void AddServicesRegistry(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();
        services.AddScoped<IOrderItemService,OrderItemService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();


    }
}