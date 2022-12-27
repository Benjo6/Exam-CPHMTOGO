using Core.Service;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Domain.Model;

namespace OrderService.Services.Interfaces;

public interface IOrderStatusService : IBaseService<OrderStatus, OrderStatusDto>
{
    public Task<OrderStatusDto> StartOrder(StartOrderStatusModel model);
    public Task<OrderStatusDto> CloseOrder(Guid id);



}