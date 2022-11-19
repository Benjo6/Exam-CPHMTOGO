namespace OrderService.Domain.Dto;

public class OrderItemDto
{
    public Guid Id { get; set; }
    public string Preference { get; set; } = default!;
    public float Price { get; set; }
    public int Quantity { get; set; }
    public Order Order { get; set; } = default!;

}