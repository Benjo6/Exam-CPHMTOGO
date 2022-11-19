using AutoMapper;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderStatusService : BaseService<OrderStatus,OrderStatusDto>,IOrderStatusService
{
    public OrderStatusService(IOrderStatusRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}