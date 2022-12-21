using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Domain;
using OrderService.Infrastructure;
using OrderService.Repositories;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.Integration_Tests.Repositories;

public class OrderRepositoryTests
{
    private DbContextOptions<RepositoryContext> _dbContextOptions;
    private IOrderRepository _repository;


    [SetUp]
    public async Task Setup()
    {
        var dbName = $"AuthenticationDb_{DateTime.Now.ToFileTimeUtc()}";
        _dbContextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        _repository = await CreateRepositoryAsync();
    }

    private async Task<IOrderRepository> CreateRepositoryAsync()
    {
            RepositoryContext context = new RepositoryContext(_dbContextOptions);
            var logger = new Mock<ILogger<OrderRepository>>();
            return new OrderRepository(context,logger.Object);
            
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var item = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
        };
        var item2 = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
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

        var item = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
        };

        _repository.Create(item);
        var okresult = await _repository.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.CustomerId, Is.EqualTo(item.CustomerId));
        Assert.That(okresult.Address, Is.EqualTo(item.Address));
        Assert.That(okresult.EmployeeId, Is.EqualTo(item.EmployeeId));
        Assert.That(okresult.RestaurantId, Is.EqualTo(item.RestaurantId));
        Assert.That(okresult.OrderStatusId, Is.EqualTo(item.OrderStatusId));
    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        var item = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
        };
        //Act
        var okresult = _repository.Create(item);


        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.CustomerId, Is.EqualTo(item.CustomerId));
        Assert.That(okresult.Address, Is.EqualTo(item.Address));
        Assert.That(okresult.EmployeeId, Is.EqualTo(item.EmployeeId));
        Assert.That(okresult.RestaurantId, Is.EqualTo(item.RestaurantId));
        Assert.That(okresult.OrderStatusId, Is.EqualTo(item.OrderStatusId));
    }  


    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        var item = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
        };
        //Act
        var orderitem = _repository.Create(item);
        orderitem.CustomerId = new Guid("630c0e49-2d28-4146-ae30-53ec48d280ae");
        _repository.Update(orderitem);
        var okresult = await _repository.GetById(item.Id);

        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(orderitem.Id));
        Assert.That(okresult.CustomerId, Is.EqualTo(new Guid("630c0e49-2d28-4146-ae30-53ec48d280ae")));
        Assert.That(okresult.Address, Is.EqualTo(orderitem.Address));
        Assert.That(okresult.EmployeeId, Is.EqualTo(orderitem.EmployeeId));
        Assert.That(okresult.RestaurantId, Is.EqualTo(orderitem.RestaurantId));
        Assert.That(okresult.OrderStatusId, Is.EqualTo(orderitem.OrderStatusId));

    }

    [Test]
    public async Task Delete_ReturnTrue()
    {
        var item = new Order()
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid(),
            RestaurantId =Guid.NewGuid()
        };

        _repository.Create(item);
        _repository.Delete(item.Id);
        var result= await _repository.GetById(item.Id);
        Assert.Null(result);

    }
    
}