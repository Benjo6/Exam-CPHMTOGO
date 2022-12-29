namespace AuthenticationService.Application.Contracts.Commands;

public record LoginInfoCommand(Guid Id, string Username, string Email);