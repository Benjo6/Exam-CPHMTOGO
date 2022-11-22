using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class OrderDto : BaseEntityDto
{
    public string Address { get; set; }
    public string CustomerId { get; set; }
    public string EmployeeId { get; set; }
    public string RestaurantId { get; set; }
    public OrderStatus OrderStatus { get; set; }

}