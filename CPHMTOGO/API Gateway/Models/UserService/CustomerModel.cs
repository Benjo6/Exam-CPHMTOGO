namespace APIGateway.Models.UserService;

public record CustomerModel(Guid Id, string FirstName, string LastName, int Phone,DateTime BirtDate,Guid Address, Guid LoginInfoId);