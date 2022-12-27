namespace APIGateway.Controllers;

public record AddressModel(Guid Id,string Street,string StreetNr,string ZipCode, string? Etage,string? Door, double Longitude,double Latitude);