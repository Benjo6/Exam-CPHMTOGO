namespace OrderService.Domain.Dto;

public class CreateOrderItemDto
{
    public string Preference { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}