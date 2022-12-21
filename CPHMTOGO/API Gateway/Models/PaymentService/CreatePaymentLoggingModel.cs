namespace APIGateway.Models.PaymentService;

public record CreatePaymentLoggingModel(Guid From, Guid To,double Amount, string Type);