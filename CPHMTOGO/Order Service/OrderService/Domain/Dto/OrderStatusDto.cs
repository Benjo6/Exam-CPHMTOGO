namespace OrderService.Domain.Dto;

public class OrderStatusDto
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public Status Status { get; set; }
}