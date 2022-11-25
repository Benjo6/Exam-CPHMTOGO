using System.ComponentModel.DataAnnotations;

namespace OrderService.Domain.Dto;

public class CreateOrderItemDto
{
    
    public string Preference { get; set; }
    [Required(ErrorMessage = "There is no price on the item or price is invalid")]
    [Range(1,100000)]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "The quantity is invalid")]
    [Range(1,100)]
    public int Quantity { get; set; }
}