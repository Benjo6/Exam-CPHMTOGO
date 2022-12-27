namespace APIGateway.Controllers;

public record CreateAddressModel(string Street, string StreetNr, string ZipCode,string? Etage, string? Door);