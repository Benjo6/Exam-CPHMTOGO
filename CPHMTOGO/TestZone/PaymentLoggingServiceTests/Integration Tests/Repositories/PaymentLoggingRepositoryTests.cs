using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Infrastructure;
using PaymentLoggingService.Repositories;
using PaymentLoggingService.Repositories.Interfaces;

namespace PaymentLoggingServiceTests.Integration_Tests.Repositories;

public class PaymentLoggingRepositoryTests
{
    private DbContextOptions<PaymentLoggingContext> _dbContextOptions;
    private IPaymentLoggingRepository _repository;


    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<PaymentLoggingContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
    }

    private async Task<IPaymentLoggingRepository> CreateRepositoryAsync()
    {
        var logger = new Mock<ILogger<PaymentLoggingRepository>>();
        PaymentLoggingContext context = new PaymentLoggingContext(_dbContextOptions);
            return new PaymentLoggingRepository(context,logger.Object);
            
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        //Arrange
        var item = new PaymentLogging
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        var item2 = new PaymentLogging
        {
            Id = Guid.NewGuid(),
            Amount = 90.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        _repository.Create(item);
        _repository.Create(item2);
        
        //Act
        _repository.GetAll();
        var okresult = _repository.GetAll();
        
        //Assert
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {
        //Arrange
        var item = new PaymentLogging
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        
        //Act
        _repository.Create(item);
         var okresult = await _repository.GetById(item.Id);
        
        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.From, Is.EqualTo(item.From));
        Assert.That(okresult.To, Is.EqualTo(item.To));
        Assert.That(okresult.Type, Is.EqualTo(item.Type));
    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        var item = new PaymentLogging
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        
        //Act
        var okresult = _repository.Create(item);
        
        //Assert   
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.From, Is.EqualTo(item.From));
        Assert.That(okresult.To, Is.EqualTo(item.To));
        Assert.That(okresult.Type, Is.EqualTo(item.Type));
    }

}