using Microsoft.EntityFrameworkCore;
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
        PaymentLoggingContext context = new PaymentLoggingContext(_dbContextOptions);
            return new PaymentLoggingRepository(context);
            
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

    [Test]
    public async Task Update_ReturnUpdatedObject()
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
        var itemPaymentLogging = _repository.Create(item);
        itemPaymentLogging.Amount = 500.0;
         _repository.Update(itemPaymentLogging);
        var okresult = await _repository.GetById(item.Id);

         //Assert
         if (okresult != null)
         {
             Assert.That(okresult.Id, Is.EqualTo(itemPaymentLogging.Id));
             Assert.That(okresult.Amount, Is.EqualTo(itemPaymentLogging.Amount));
             Assert.That(okresult.From, Is.EqualTo(itemPaymentLogging.From));
             Assert.That(okresult.To, Is.EqualTo(itemPaymentLogging.To));
             Assert.That(okresult.Type, Is.EqualTo(itemPaymentLogging.Type));
         }
    }

    [Test]
    public async Task Delete_ReturnTrue()
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
        _repository.Create(item);
        
        //Act
        _repository.Delete(item.Id);
        var result= await _repository.GetById(item.Id);
        
        //Assert
        Assert.Null(result);

    }
    
}