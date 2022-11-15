using AuthenticationService.Application.Contracts.Commands;

namespace AuthenticationService.Application;

public interface IAuthenticationApplicationRepository
{
    Task<bool> SignIn(SignInCommand request);
    Task<bool> SignUp(SignUpCommand request);
    Task<bool> ChangePassword(ChangePasswordCommand request);
}