namespace APIGateway.Models.PaymentService;

public record PaymentCreateChargeModel( long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description);