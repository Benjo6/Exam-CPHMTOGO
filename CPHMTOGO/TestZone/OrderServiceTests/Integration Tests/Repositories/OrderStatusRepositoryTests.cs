using Microsoft.EntityFrameworkCore;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.Integration_Tests.Repositories;

public class OrderStatusRepositoryTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions;
    private IOrderStatusRepository _repository;


    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
    }

    private async Task<IOrderStatusRepository> CreateRepositoryAsync()
    {
            RepositoryContext context = new RepositoryContext(_dbContextOptions);
            return new OrderStatusRepository(context);
            
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var item = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status= "STARTED",
            TimeStamp = DateTime.UtcNow
        };
        var item2 = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status= "STARTED",
            TimeStamp = DateTime.UtcNow
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

        var item = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status= "STARTED",
            TimeStamp = DateTime.UtcNow
        };

        _repository.Create(item);
        var okresult = await _repository.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Status, Is.EqualTo(item.Status));
        Assert.That(okresult.TimeStamp, Is.EqualTo(item.TimeStamp));


    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        var item = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status= "STARTED",
            TimeStamp = DateTime.UtcNow
        };
        //Act
        var okresult = _repository.Create(item);


        //Assert
        Assert.That(okresult.Status, Is.EqualTo(item.Status));
        Assert.That(okresult.TimeStamp, Is.EqualTo(item.TimeStamp));
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        var item = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status= "STARTED",
            TimeStamp = DateTime.UtcNow
        };
        //Act
        var itemStatus = _repository.Create(item);
        itemStatus.Status = "DELIVERED";
        _repository.Update(item);
        var okresult = await _repository.GetById(item.Id);

        //Assert
        if (okresult != null)
        {
            Assert.That(okresult.Status, Is.EqualTo("DELIVERED"));
            Assert.That(okresult.TimeStamp, Is.EqualTo(item.TimeStamp));
            Assert.That(okresult.Id, Is.EqualTo(item.Id));
        }
    }

    [Test]
    public async Task Delete_ReturnTrue()
    {
        var item = new OrderStatus()
        {
            Id = Guid.NewGuid(),
            Status = "STARTED",
            TimeStamp= DateTime.UtcNow
        };

        _repository.Create(item);
        _repository.Delete(item.Id);
        var result= await _repository.GetById(item.Id);
        Assert.Null(result);

    }
    
}