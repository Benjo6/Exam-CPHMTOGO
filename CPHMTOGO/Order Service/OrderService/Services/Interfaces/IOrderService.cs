using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Services.Interfaces;

public interface IOrderService : IBaseService<Order,OrderDto>
{
    public Task<OrderDto> CreateOrderTask(OrderDto entity, List<CreateOrderItemDto> itemDtos);

}