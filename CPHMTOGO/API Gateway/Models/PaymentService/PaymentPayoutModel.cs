namespace APIGateway.Models.PaymentService;

public record PaymentPayoutModel(string Currency, long  Amount,string Description);