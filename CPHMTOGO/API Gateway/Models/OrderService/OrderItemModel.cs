namespace APIGateway.Models.OrderService;

public record OrderItemModel(string Preference, double Price, int Quantity, Guid OrderId,Guid Id);