namespace APIGateway.Models.OrderService;

public record OrderModel(Guid AddressId, Guid CustomerId,Guid EmployeeId,Guid RestaurantId,Guid OrderStatusId,Guid Id);
