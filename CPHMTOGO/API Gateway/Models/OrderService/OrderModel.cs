namespace APIGateway.Models.OrderService;

public record OrderModel(Guid Address, Guid CustomerId,Guid EmployeeId,Guid RestaurantId,Guid OrderStatusId,Guid Id);
