namespace PaymentService.Resources;

public record CreateCustomerResource(
    string Email,
    string Name, 
    CreateCardResource Card);