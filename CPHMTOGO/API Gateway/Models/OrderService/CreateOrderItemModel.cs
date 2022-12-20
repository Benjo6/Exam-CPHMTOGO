namespace APIGateway.Models.OrderService;

public record CreateOrderItemModel( String Preference, Double Price, int Quantity, Guid OrderId);