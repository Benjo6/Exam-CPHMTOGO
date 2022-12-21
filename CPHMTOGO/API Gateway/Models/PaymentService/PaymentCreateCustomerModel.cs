using APIGateway.Models.PaymentService;

namespace APIGateway.Models;

public record PaymentCreateCustomerModel(string Email,
    string Name, 
    PaymentCreateCardModel Card);