using Core.Service;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;

namespace OrderService.Services.Interfaces;

public interface IOrderStatusService : IBaseService<OrderStatus, OrderStatusDto>
{
    public Task<OrderStatusDto> StartOrder(Guid id, Guid employeeId);
    public Task<OrderStatusDto> CloseOrder(Guid id);


}