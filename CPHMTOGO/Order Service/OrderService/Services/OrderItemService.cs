using AutoMapper;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderItemService : BaseService<OrderItem,OrderItemDto>,IOrderItemService
{
    public OrderItemService(IOrderItemRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
    
    
}