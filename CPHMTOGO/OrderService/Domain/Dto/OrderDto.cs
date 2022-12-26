using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Domain.Dto;

public class OrderDto : BaseEntityDto
{
    [Required(ErrorMessage = "Address is missing for order")]
    public Guid Address { get; set; }
    [Required(ErrorMessage = "Customer is missing for order")]
    public Guid CustomerId { get; set; }
    [Required(ErrorMessage = "Restaurant is missing for order")]
    public Guid RestaurantId { get; set; }
    [Required(ErrorMessage="OrderStatus is missing for order")]
    public Guid OrderStatusId { get; set; }
}
public class CreateOrderDto 
{
    [Required(ErrorMessage = "Address is missing for order")]
    public Guid Address { get; set; }
    [Required(ErrorMessage = "Customer is missing for order")]
    public Guid CustomerId { get; set; }
    [Required(ErrorMessage = "Restaurant is missing for order")]
    public Guid RestaurantId { get; set; }
    public List<CreateOrderItemDto> OrderItems { get; set; }


}