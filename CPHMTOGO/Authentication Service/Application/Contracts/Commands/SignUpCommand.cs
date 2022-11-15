namespace AuthenticationService.Application.Contracts.Commands;

public class SignUpCommand
{
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}