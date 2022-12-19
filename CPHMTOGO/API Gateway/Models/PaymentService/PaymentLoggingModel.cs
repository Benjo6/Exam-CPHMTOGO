namespace APIGateway.Models.PaymentService;

public record PaymentLoggingModel(string From, string To, string Type, string Amount, string Id);
