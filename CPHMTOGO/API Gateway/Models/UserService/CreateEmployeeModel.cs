namespace APIGateway.Models.UserService;

public record CreateEmployeeModel(string firstname, string lastname,bool active, Guid loginInfoId,Guid address,int kontoNr,int regNr);