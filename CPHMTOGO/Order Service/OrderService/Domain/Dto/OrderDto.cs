namespace OrderService.Domain.Dto;

public class OrderDto
{
    public Guid Id { get; set; }
    public string Address { get; set; } = default!;
    public string CustomerId { get; set; } = default!;
    public string EmployeeId { get; set; } = default!;
    public string RestaurantId { get; set; } = default!;
    public OrderStatus OrderStatus { get; set; } = default!;
}