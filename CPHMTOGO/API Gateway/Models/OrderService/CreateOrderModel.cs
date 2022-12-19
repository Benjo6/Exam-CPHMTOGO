namespace APIGateway.Models.OrderService;

public record CreateOrderModel(Guid Address, Guid CustomerId, Guid RestaurantId, List<OrderItemModel> OrderItems);