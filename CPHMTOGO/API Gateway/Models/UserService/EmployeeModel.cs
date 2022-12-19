namespace APIGateway.Models.UserService;

public record EmployeeModel(Guid Id,string FirstName, string LastName,bool Active, Guid LoginInfoId,Guid Address,int KontoNr,int RegNr );