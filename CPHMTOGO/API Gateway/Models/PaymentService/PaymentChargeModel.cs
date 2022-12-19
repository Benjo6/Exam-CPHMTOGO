namespace APIGateway.Models.PaymentService;

public record PaymentChargeModel(
    string ChargeId,
    string Currency,
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description);