namespace APIGateway.Models.PaymentService;

public record PaymentCustomerModel(
    string CustomerId,
    string Email,
    string Name);