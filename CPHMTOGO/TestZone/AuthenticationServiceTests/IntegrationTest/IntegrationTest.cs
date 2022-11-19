using AuthenticationService.Application;
using AuthenticationService.Application.Contracts.Commands;
using AuthenticationService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServiceTests.IntegrationTest;

public class IntegrationTest
{
    private DbContextOptions<AuthenticationDbContext> _dbContextOptions;
    private AuthenticationApplicationRepository _repository;

    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<AuthenticationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
    }

    [Test]
    public async Task SuccessfulSignUp()
    {
        //Arrange
        var username = "igor";
        var password = "husmus";
        var email = "e@e.dk";
        //Act
        var response = await _repository.SignUp(new SignUpCommand{Email = email,Password = password,Username = username});
        //Assert
        Assert.True(response);
    }

    [Test]
    public async Task UserAlreadyExistSignuUp()
    {
        //Arrange
        var username = "igor";
        var password = "husmus";
        var email = "e@e.dk";
        
        //Act
        await _repository.SignUp(new SignUpCommand{Email = email,Password = password,Username = username});

        //Arrange
        var ex = Assert.ThrowsAsync<Exception>(async () =>await 
            _repository.SignUp(new SignUpCommand { Email = email, Password = password, Username = username }));
        Assert.That(ex.Message, Is.EqualTo("Username already exists"));

    }

    [Test]
    public async Task SuccessfulLogin()
    {
        //Arrange
        var username = "igor";
        var password = "husmus";
        

        //Act
        await _repository.SignUp(new SignUpCommand{Email = "e@e.dk",Password = password,Username = username});
        var response = await _repository.SignIn(new SignInCommand{Password = password,Username = username});
        
        //Assert
        Assert.True(response);
    }
    
    [Test]
    public async Task FailedLogin()
    {
        //Arrange
        var username = "igor";
        var password = "husmus";
        var failedPassword = "hus";

        //Act
        await _repository.SignUp(new SignUpCommand{Email = "e@e.dk",Password = password,Username = username});

        var response = await _repository.SignIn(new SignInCommand{Password = failedPassword,Username = username});
        
        //Assert
        Assert.False(response);
    }

    [Test]
    public async Task ChangePasswordSuccessfully()
    {
        //Arrange
        var username = "igor";
        var password = "hus";
        var newPassword = "husmus";

        //Act
        await _repository.SignUp(new SignUpCommand{Email = "e@e.dk",Password = password,Username = username});

        var response = await _repository.ChangePassword(new ChangePasswordCommand{NewPassword = password,Username = username});
        
        //Assert
        Assert.True(response);
        var login = _repository.SignIn(new SignInCommand{Password = password,Username = username}).Result;
        Assert.True(login);
    }
    [Test]
    public async Task ChangePasswordForAUserThatDontExist()
    {
        //Arrange
        var username = "benjo";
        var password = "hus";
        
        //Act
        var response = await _repository.ChangePassword(new ChangePasswordCommand
            { NewPassword = password, Username = username });

        //Assert
        Assert.False(response);
    }

    private async Task<AuthenticationApplicationRepository> CreateRepositoryAsync()
    {
        AuthenticationDbContext context = new AuthenticationDbContext(_dbContextOptions);
        return new AuthenticationApplicationRepository(context);
    }
    
}