using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class OrderItemDto : BaseEntityDto
{
    public string Preference { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    
}