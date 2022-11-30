using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class OrderItemControllerTests
{
    private Mock<IOrderItemService> _service;
    private OrderItemController _controller;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IOrderItemService>();
        _controller = new OrderItemController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderItemDto>()
        {
            new() { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1},
            new() { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 2},
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as List<OrderItemDto>;

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new OrderItemDto 
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};

        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as OrderItemDto;
        
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task CreateOrderItem_ReturnCreatedObject()
    {
        OrderItemDto? dto = null;
        _service.Setup(r => r.Create(It.IsAny<OrderItemDto>())).Callback<OrderItemDto>(r => dto = r);

        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};

        await _controller.Post(item);
        _service.Verify(x=> x.Create(It.IsAny<OrderItemDto>()),Times.Once);
        
        Assert.That(item.Preference, Is.EqualTo(dto.Preference));
        Assert.That(item.Price, Is.EqualTo(dto.Price));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
        Assert.That(item.Quantity, Is.EqualTo(dto.Quantity));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        OrderItemDto? dto = null;

        _service.Setup(r => r.Update(It.IsAny<OrderItemDto>()).Result).Callback<OrderItemDto>(r=>dto=r);
        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 600.50,Quantity = 1};
        //Act
        await _controller.Put(item);
        _service.Verify(x=> x.Update(It.IsAny<OrderItemDto>()),Times.Once);


        //Assert
        Assert.That(item.Preference, Is.EqualTo(dto.Preference));
        Assert.That(item.Price, Is.EqualTo(dto.Price));
        Assert.That(item.Quantity, Is.EqualTo(dto.Quantity));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};
        _service.Setup(x => x.Delete(item.Id).Result).Returns(true);

        var okresult =  _controller.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}