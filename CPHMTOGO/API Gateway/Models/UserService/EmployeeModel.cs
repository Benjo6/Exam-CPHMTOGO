namespace APIGateway.Models.UserService;

public record EmployeeModel(string id,string firstname, string lastname,bool active, Guid loginInfoId,Guid address,int kontoNr,int regNr, string accountId);
