namespace PaymentService.Resources;

public record CreateChargeResource(
    long Amount,
    string CustomerId,
    string ReceiptEmail,
    string Description);