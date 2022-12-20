namespace APIGateway.Models.PaymentService;

public record PaymentCreateCardModel(string Name,
    string Number,
    string ExpiryYear,
    string ExpiryMonth,
    string CvC);