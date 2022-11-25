using Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Domain.Dto;

public class OrderDto : BaseEntityDto
{
    public Guid Address { get; set; }
    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid OrderStatusId { get; set; }
    

}