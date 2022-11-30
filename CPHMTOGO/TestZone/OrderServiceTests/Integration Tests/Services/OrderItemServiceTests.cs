using AutoMapper;
using Moq;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Services;

public class OrderItemServiceTests
{
    private Mock<IOrderItemRepository> _repository;
    private IOrderItemService _service;
    private IMapper _mapper;


    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>());
        _repository = new Mock<IOrderItemRepository>();
        _mapper = new Mapper(config);
        _service = new OrderItemService(_repository.Object, _mapper);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderItem>()
        {
            new() { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1},
            new() { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1},
            new() { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1}
        };
        _repository.Setup(x => x.GetAll()).Returns(items.AsQueryable);

        var okresult = await _service.GetAll();
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new OrderItem()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1};
        
        _repository.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _service.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(item.Preference, Is.EqualTo(okresult.Preference));
        Assert.That(item.Price, Is.EqualTo(okresult.Price));
        Assert.That(item.Quantity, Is.EqualTo(okresult.Quantity));
        Assert.That(item.OrderId, Is.EqualTo(okresult.OrderId));
        Assert.That(item.Id, Is.EqualTo(okresult.Id));


    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        OrderItem? dto = null;

        _repository.Setup(r => r.Create(It.IsAny<OrderItem>())).Callback<OrderItem>(r=>dto=r);
        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1};
        //Act
        await _service.Create(item);
        _repository.Verify(x=> x.Create(It.IsAny<OrderItem>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Preference, Is.EqualTo(dto.Preference));
        Assert.That(item.Price, Is.EqualTo(dto.Price));
        Assert.That(item.Quantity, Is.EqualTo(dto.Quantity));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        OrderItem? dto = null;

        _repository.Setup(r => r.Update(It.IsAny<OrderItem>())).Callback<OrderItem>(r=>dto=r);
        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1};
        //Act
        await _service.Update(item);
        _repository.Verify(x=> x.Update(It.IsAny<OrderItem>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Preference, Is.EqualTo(dto.Preference));
        Assert.That(item.Price, Is.EqualTo(dto.Price));
        Assert.That(item.Quantity, Is.EqualTo(dto.Quantity));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1};
        _repository.Setup(x => x.Delete(item.Id));

        var okresult =  _service.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}