namespace APIGateway.Models.UserService;

public record NewCompanyModel(string id, string name,int kontoNr,int regNr, Guid loginInfoId);
public record CompanyModel(string id, string name,int kontoNr,int regNr);