namespace APIGateway.Controllers;

public record AddressModel(Guid Id,string Street,string StreetNr,string ZipCode, double Longitude,double Latitude);