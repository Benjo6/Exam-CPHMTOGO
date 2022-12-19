namespace APIGateway.Controllers;

public record CreateAddressModel(string Street, string StreetNr, string ZipCode,string? etage, string? door);