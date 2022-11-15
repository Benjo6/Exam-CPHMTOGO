using AuthenticationService.Application;
using AuthenticationService;
using AuthenticationService.Application;
using AuthenticationService.Infrastructure;
using AuthenticationService.Service;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Core.Testing;
using Moq;
using service;

namespace AuthenticationServiceUnitTest
{
    [TestFixture]
    public class Tests
    {
        private IMapper _mapper;
        private AuthenticationService.Service.AuthenticationService _service;
        private Mock<IAuthenticationApplicationRepository> _mockRepository;
        private AuthenticationDbContext _context;

        [SetUp]
        public void Setup()
        {
        }

  

        [Test]
        public void ShouldValidateSignIn()
        {
            //Arrange
            var username = "benjo";
            var password = "husmus";
            var context = TestServerCallContext.Create(
                method: nameof(IAuthenticationApplicationRepository.SignIn),
                host: "localhost",
                deadline: DateTime.Now.AddMinutes(30),
                requestHeaders: new Metadata(),
                cancellationToken: CancellationToken.None,
                peer: "10.0.0.25:5001",
                authContext: null,
                contextPropagationToken: null,
                writeHeadersFunc: (metadata) => Task.CompletedTask,
                writeOptionsGetter: () => new WriteOptions(),
                writeOptionsSetter: (writeOptions) => { }
                );
            //Act
            var login = _service.SignIn(new SignInRequest() { Username = username, Password = password },context);

            //Assert
            Assert.That(login.Result, Is.EqualTo(new BoolValue () { Value=true}));

        }

        [Test]
        public void ShouldFailSignIn()
        {
            //Arrange
            var username = "benjo";
            var password = "hus";

            //Act
            var login = _mockRepository.Object.SignIn(new AuthenticationService.Application.Contracts.Commands.SignInCommand { Username = username, Password = password });

            //Assert
            Assert.That(login.Result, Is.EqualTo(false));


        }
        [Test]
        public void ShouldValidateSignUp()
        {
            //Arrange
            var email = "main@mail.dk";
            var username = "benjo1";
            var password = "husmus";
            var context = TestServerCallContext.Create(
                method: nameof(IAuthenticationApplicationRepository.SignUp),
                host: "localhost",
                deadline: DateTime.Now.AddMinutes(30),
                requestHeaders: new Metadata(),
                cancellationToken: CancellationToken.None,
                peer: "10.0.0.25:5001",
                authContext: null,
                contextPropagationToken: null,
                writeHeadersFunc: (metadata) => Task.CompletedTask,
                writeOptionsGetter: () => new WriteOptions(),
                writeOptionsSetter: (writeOptions) => { }
                );

            //Act
            var signup = _service.SignUp(new SignUpRequest(){Username=username,Email=email,Password=password},context);

            //Assert
            Assert.That(signup.Result, Is.EqualTo(new BoolValue() { Value=true}));
        }
        [Test]
        public void ShouldFailBecauseUserAlreadyExistInSignUp()
        {
            //Arrange
            var email = "main@mail.dk";
            var username = "benjo";
            var password = "husmus";

            //Act
            //Assert
            Assert.Throws<Exception>(() => _mockRepository.Object.SignUp(new AuthenticationService.Application.Contracts.Commands.SignUpCommand { Username = username, Password = password, Email = email }));
            
        }

    }
}