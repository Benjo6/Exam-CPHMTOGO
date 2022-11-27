using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Services.Interfaces;

public interface IOrderItemService : IBaseService<OrderItem,OrderItemDto>
{
    
}