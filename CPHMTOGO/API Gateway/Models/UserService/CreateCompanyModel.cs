namespace APIGateway.Models.UserService;

public record CreateCompanyModel(string Name,int KontoNr,int RegNr, Guid LoginInfoId);
