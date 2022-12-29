using AuthenticationService.Application.Contracts.Commands;
using AuthenticationService.Domain;

namespace AuthenticationService.Application;

public interface IAuthenticationApplicationRepository
{
    Task<bool> SignIn(SignInCommand request);
    Task<bool> SignUp(SignUpCommand request);
    Task<bool> ChangePassword(ChangePasswordCommand request);
    Task<LoginInfo> GetById(Guid id);
}