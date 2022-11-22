using AutoMapper;
using Core.Repository;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

class OrderStatusService : BaseService<OrderStatus,OrderStatusDto> ,IOrderStatusService
{
    public OrderStatusService(IOrderStatusRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public override async Task<OrderStatusDto> Create(OrderStatusDto entityDto)
    {
        entityDto.Status = Status.STARTED;
        entityDto.TimeStamp=DateTime.Now;

        return entityDto;
    }
}