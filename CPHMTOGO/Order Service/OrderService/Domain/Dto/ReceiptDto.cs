using System.ComponentModel.DataAnnotations;
using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class ReceiptDto : BaseEntityDto
{
    [Required(ErrorMessage = "The amount is invalid for Receipt")]
    [Range(1,Int32.MaxValue)]
    public double Amount { get; set; }
    [Required(ErrorMessage = "The time isn't set for Receipt")]
    public DateTime Time { get; set; }
    [Required(ErrorMessage = "order isn't set for Receipt")]
    public Guid OrderId { get; set; }    
    

}