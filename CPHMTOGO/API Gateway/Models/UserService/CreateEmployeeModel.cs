namespace APIGateway.Models.UserService;

public record CreateEmployeeModel(string FirstName, string LastName,bool Active, Guid LoginInfoId,Guid Address,int KontoNr,int RegNr);