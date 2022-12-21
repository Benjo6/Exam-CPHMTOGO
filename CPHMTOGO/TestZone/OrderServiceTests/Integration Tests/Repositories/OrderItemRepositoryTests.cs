using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace OrderServiceTests.Integration_Tests.Repositories;

public class OrderItemRepositoryTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions;
    private IOrderItemRepository _repository;


    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
        
    }

    private async Task<IOrderItemRepository> CreateRepositoryAsync()
    {
         var logger = new Mock<ILogger<OrderItemRepository>>();
            RepositoryContext context = new RepositoryContext(_dbContextOptions);
            return new OrderItemRepository(context,logger.Object);
            
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var item = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 20.50,
            Quantity = 3

        };
        var item2 = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 40.50,
            Quantity = 3

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

        var item = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 20.50,
            Quantity = 3

        };

        _repository.Create(item);
        var okresult = await _repository.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.OrderId, Is.EqualTo(item.OrderId));
        Assert.That(okresult.Preference, Is.EqualTo(item.Preference));
        Assert.That(okresult.Price, Is.EqualTo(item.Price));
        Assert.That(okresult.Quantity, Is.EqualTo(item.Quantity));
        
    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        var item = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 20.50,
            Quantity = 3

        };
        //Act
        var okresult = _repository.Create(item);


        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.OrderId, Is.EqualTo(item.OrderId));
        Assert.That(okresult.Preference, Is.EqualTo(item.Preference));
        Assert.That(okresult.Price, Is.EqualTo(item.Price));
        Assert.That(okresult.Quantity, Is.EqualTo(item.Quantity)); 
    }  


    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        var item = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 20.50,
            Quantity = 3

        };
        //Act
        var orderitem = _repository.Create(item);
        orderitem.Preference = "Pizza";
        _repository.Update(orderitem);
        var okresult = await _repository.GetById(item.Id);

        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(orderitem.Id));
        Assert.That(okresult.OrderId, Is.EqualTo(orderitem.OrderId));
        Assert.That(okresult.Preference, Is.EqualTo(orderitem.Preference));
        Assert.That(okresult.Price, Is.EqualTo(orderitem.Price));
        Assert.That(okresult.Quantity, Is.EqualTo(orderitem.Quantity)); 

    }

    [Test]
    public async Task Delete_ReturnTrue()
    {
        var item = new OrderItem()
        {
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Preference = "",
            Price = 20.50,
            Quantity = 3

        };

        _repository.Create(item);
        _repository.Delete(item.Id);
        var result= await _repository.GetById(item.Id);
        Assert.Null(result);

    }
    
}