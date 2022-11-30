using Microsoft.EntityFrameworkCore;
using Moq;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.Integration_Tests.Repositories;

public class ReceiptRepositoryTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions;
    private IReceiptRepository _repository;


    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
    }

    private async Task<IReceiptRepository> CreateRepositoryAsync()
    {
            RepositoryContext context = new RepositoryContext(_dbContextOptions);
            return new ReceiptRepository(context);
            
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var item = new Receipt
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        var item2 = new Receipt
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        _repository.Create(item);
        _repository.Create(item2);

        _repository.GetAll();
        var okresult = _repository.GetAll();
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new Receipt
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };

         _repository.Create(item);


        var okresult = await _repository.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.Time, Is.EqualTo(item.Time));
        Assert.That(okresult.OrderId, Is.EqualTo(item.OrderId));


    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange

        var item = new Receipt()
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        
        var okresult = _repository.Create(item);


        //Assert
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.Time, Is.EqualTo(item.Time));
        Assert.That(okresult.OrderId, Is.EqualTo(item.OrderId));
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        var item = new Receipt()
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        var itemReceipt = _repository.Create(item);
        itemReceipt.Amount = 500.0;
         _repository.Update(itemReceipt);
        var okresult = await _repository.GetById(item.Id);

         //Assert
         if (okresult != null)
         {
             Assert.That(okresult.Amount, Is.EqualTo(500.0));
             Assert.That(okresult.Time, Is.EqualTo(itemReceipt.Time));
             Assert.That(okresult.OrderId, Is.EqualTo(itemReceipt.OrderId));
             Assert.That(okresult.Id, Is.EqualTo(itemReceipt.Id));
         }
    }

    [Test]
    public async Task Delete_ReturnTrue()
    {
        var item = new Receipt()
        {
            Id = Guid.NewGuid(),
            Amount = 56.65,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        _repository.Create(item);
        _repository.Delete(item.Id);
        var result= await _repository.GetById(item.Id);
        Assert.Null(result);

    }
    
}