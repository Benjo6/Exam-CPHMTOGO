using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class OrderControllerTests
{
    private Mock<IOrderService> _service;
    private OrderController _controller;
    private Mock<ILogger<OrderController>> _logger;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IOrderService>();
        _controller = new OrderController(_service.Object,_logger.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderDto>()
        {
            new OrderDto {RestaurantId = Guid.NewGuid(),CustomerId = Guid.NewGuid(),Address = Guid.NewGuid(),Id = Guid.NewGuid(),OrderStatusId = Guid.NewGuid()},
            new OrderDto {RestaurantId = Guid.NewGuid(),CustomerId = Guid.NewGuid(),Address = Guid.NewGuid(),Id = Guid.NewGuid(),OrderStatusId = Guid.NewGuid()},
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as List<OrderDto>;

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
             Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as OrderDto;
        
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task CreateOrder_ShouldReturnCorrectResult()
    {
        // Arrange
        var address = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var restaurantId = Guid.NewGuid();
        var orderDtos = new List<CreateOrderItemDto>();
        var expectedResult = new OrderDto
        {
            Address = address,CustomerId = customerId,RestaurantId = restaurantId
        };

        // Configure the mock service to return the expected result when CreateOrderTask is called
        _service
            .Setup(service => service.CreateOrderTask(It.IsAny<CreateOrderDto>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.CreateOrder(new CreateOrderDto(){Address = address,CustomerId = customerId,RestaurantId = restaurantId,OrderItems = orderDtos});

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        OrderDto? dto = null;

        _service.Setup(r => r.Update(It.IsAny<OrderDto>()).Result).Callback<OrderDto>(r=>dto=r);
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        //Act
        await _controller.Put(item);
        _service.Verify(x=> x.Update(It.IsAny<OrderDto>()),Times.Once);


        //Assert
        Assert.That(item.RestaurantId, Is.EqualTo(dto.RestaurantId));
        Assert.That(item.Address, Is.EqualTo(dto.Address));
        Assert.That(item.CustomerId, Is.EqualTo(dto.CustomerId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.OrderStatusId, Is.EqualTo(dto.OrderStatusId));

    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        _service.Setup(x => x.Delete(item.Id).Result).Returns(true);

        var okresult =  _controller.Delete(item.Id).Result as OkObjectResult;

        Assert.IsTrue(okresult.Value is bool ? (bool)okresult.Value : false);
    }
    [Test]
    public async Task NumberOfOpenOrders_ShouldReturnCorrectResult()
    {
        // Arrange
        var expectedResult = 5;

        // Configure the mock service to return the expected result when NumberOfOpenOrders is called
        _service
            .Setup(service => service.NumberOfOpenOrders())
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.NumberOfOpenOrders();

        // Assert
        _service.Verify(service => service.NumberOfOpenOrders(), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public async Task GetOpenOrdersForEmployees_ShouldReturnCorrectResult()
    {
        // Arrange
        var expectedResult = new List<OrderDto>
        {
            new(),
            new()
        };

        // Configure the mock service to return the expected result when GetOpenOrders is called
        _service
            .Setup(service => service.GetOpenOrders())
            .ReturnsAsync(expectedResult);

        // Act
        var result = await _controller.GetOpenOrdersForEmployees();

        // Assert
        _service.Verify(service => service.GetOpenOrders(), Times.Once());
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}