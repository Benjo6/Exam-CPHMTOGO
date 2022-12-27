namespace APIGateway.Models.UserService;

public record CustomerModel(Guid id, string firstname, string lastname, int phone,DateTime birtdate,Guid address, Guid loginInfoId);
public record CreateCustomerModel(string firstname, string lastname, int phone,DateTime birtdate,Guid address, Guid loginInfoId);