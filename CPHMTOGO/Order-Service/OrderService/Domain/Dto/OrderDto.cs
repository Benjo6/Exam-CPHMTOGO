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
    public Guid EmployeeId { get; set; }
    [Required(ErrorMessage = "Restaurant is missing for order")]
    public Guid RestaurantId { get; set; }
    [Required(ErrorMessage="OrderStatus is missing for order")]
    public Guid OrderStatusId { get; set; }
    

}