namespace AuthenticationService.Application.Contracts.Commands;

public class ChangePasswordCommand
{
    public string Username { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}