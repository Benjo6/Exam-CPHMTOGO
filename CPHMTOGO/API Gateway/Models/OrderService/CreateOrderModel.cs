namespace APIGateway.Models.OrderService;

public record CreateOrderModel(List<CreateOrderItemModel> OrderItems, Guid AddressId, Guid CustomerId, Guid RestaurantId);
