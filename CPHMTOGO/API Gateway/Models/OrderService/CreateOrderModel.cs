namespace APIGateway.Models.OrderService;

public record CreateOrderModel(List<CreateOrderItemModel> OrderItems, Guid Address, Guid CustomerId, Guid RestaurantId);
