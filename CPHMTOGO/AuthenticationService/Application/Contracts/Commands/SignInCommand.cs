namespace AuthenticationService.Application.Contracts.Commands;

public class SignInCommand
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}