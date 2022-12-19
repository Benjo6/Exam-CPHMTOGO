using AuthenticationService.Application;
using AuthenticationService.Application.Contracts.Commands;
using Moq;

namespace AuthenticationServiceTests;

public class UnitTest
{
    private Mock<IAuthenticationApplicationRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IAuthenticationApplicationRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var username = "benjo";
        var newusername = "dobro";
        var password = "husmus";
        var changepassword = "hus";
        var email = "e@e.dk";
        
        //Act

        _dot.Object.SignIn(new SignInCommand { Password = password, Username = username });
        _dot.Object.SignUp(new SignUpCommand { Email = email, Password = password, Username = newusername });
        _dot.Object.ChangePassword(new ChangePasswordCommand { NewPassword = changepassword, Username = username });
        
        //Assert
        _dot.VerifyAll();


    }
}