namespace APIGateway.Models.OrderService;

public record OrderReceiptModel(Guid Id, double Amount, DateTime Time, Guid OrderId);