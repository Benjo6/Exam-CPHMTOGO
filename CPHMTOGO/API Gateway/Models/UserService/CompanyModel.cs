namespace APIGateway.Models.UserService;

public record NewCompanyModel(string Id, string Name,int KontoNr,int RegNr,string Role, Guid LoginInfoId);
public record CompanyModel(string Id, string Name,int KontoNr,int RegNr,string Role);