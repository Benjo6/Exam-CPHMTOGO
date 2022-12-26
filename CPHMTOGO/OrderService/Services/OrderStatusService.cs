using AutoMapper;
using Core.Repository;
using Core.Service;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Domain.Model;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderService.Services;

public class OrderStatusService : BaseService<OrderStatus,OrderStatusDto> ,IOrderStatusService
{
    private IOrderRepository _orderRepository;
    public OrderStatusService(IOrderStatusRepository repository, IMapper mapper, IOrderRepository orderRepository) : base(repository, mapper)
    {
        _orderRepository = orderRepository;
    }

    public override async Task<OrderStatusDto> Create(OrderStatusDto entityDto)
    {
        entityDto.Status = Status.STARTED.ToString();
        entityDto.TimeStamp=DateTime.UtcNow;
        
        OrderStatus entity = _mapper.Map<OrderStatus>(entityDto);

        _repository.Create(entity);

        entityDto = _mapper.Map<OrderStatusDto>(entity);

        return entityDto;
    }

    public Task<OrderStatusDto> StartOrder(StartOrderStatusModel model)
    {
        var order = _orderRepository.GetById(model.OrderId).Result;
        order.EmployeeId = model.EmployeeId;
        _orderRepository.Update(order);
        OrderStatusDto entityDto = GetById(order.OrderStatusId).Result;
        entityDto.TimeStamp=DateTime.UtcNow;
        entityDto.Status = Status.DELIVERING.ToString();
        _repository.Update(_mapper.Map<OrderStatus>(entityDto));
        return _mapper.Map<Task<OrderStatusDto>>(entityDto);
    }

    public Task<OrderStatusDto> CloseOrder(Guid orderid)
    {
        var order = _orderRepository.GetById(orderid).Result;
        OrderStatusDto entityDto = GetById(order.OrderStatusId).Result;
        entityDto.TimeStamp=DateTime.UtcNow;
        entityDto.Status = Status.DELIVERED.ToString();
        _repository.Update(_mapper.Map<OrderStatus>(entityDto));
        return _mapper.Map<Task<OrderStatusDto>>(entityDto);
    }

    
}