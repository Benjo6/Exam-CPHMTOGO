using Core.Entity.Dtos;

namespace OrderService.Domain.Dto;

public class OrderItemDto : BaseEntityDto
{
    public string Preference { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; }

}