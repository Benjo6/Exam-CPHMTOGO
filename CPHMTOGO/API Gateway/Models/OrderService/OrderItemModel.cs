namespace APIGateway.Models.OrderService;

public record OrderItemModel(String Preference, Double Price, int Quantity, Guid OrderId,Guid Id);