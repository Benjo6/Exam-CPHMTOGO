namespace APIGateway.Models.UserService;

public record EmployeeModel(string Id,string FirstName, string LastName,bool Active, Guid LoginInfoId,Guid Address,int KontoNr,int RegNr );