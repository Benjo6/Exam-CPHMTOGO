namespace APIGateway.Models.UserService;

public record NewCompanyModel(Guid Id, string Name,int KontoNr,int RegNr,string Role, Guid LoginInfoId);
public record CompanyModel(Guid Id, string Name,int KontoNr,int RegNr,string Role);