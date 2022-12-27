namespace APIGateway.Models.UserService;

public record CreateCompanyModel(string name,int kontoNr,int regNr, Guid loginInfoId);

