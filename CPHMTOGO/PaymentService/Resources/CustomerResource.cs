namespace PaymentService.Resources;

public record CustomerResource(
    string CustomerId,
    string Email,
    string Name);