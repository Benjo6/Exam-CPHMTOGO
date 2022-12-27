using System.Diagnostics;
using AutoMapper;
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

        // Setup mock methods for IOrderService
        _service.Setup(x => x.CreateOrderTask(It.IsAny<CreateOrderDto>())).ReturnsAsync(new OrderDto());
        _service.Setup(x => x.GetOpenOrders()).ReturnsAsync(new List<OrderDto>());
        _service.Setup(x => x.NumberOfOpenOrders()).ReturnsAsync(0);
        _service.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new OrderDto());
        _service.Setup(x => x.GetAll()).ReturnsAsync(new List<OrderDto>());
        _service.Setup(x => x.Update(It.IsAny<OrderDto>())).ReturnsAsync(new OrderDto());
        _service.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
        

        // Assign mock ILogger to _logger
        _logger = new Mock<ILogger<OrderController>>();

        // Create instance of OrderController with mock IOrderService and IMapper
        _controller = new OrderController(_service.Object, _logger.Object);
    }

    [Test]
    public async Task OrderController_Get_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderController_GetById_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderController_CreateOrder_ReturnsOkResult()
    {
        // Arrange
        var dto = new CreateOrderDto()
        {
            AddressId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            RestaurantId = Guid.NewGuid(),
            OrderItems = new List<CreateOrderItemDto>()
        };

        // Act
        var result = await _controller.CreateOrder(dto);

        // Assert
        Assert.IsInstanceOf<OrderDto>(result);
    }
    [Test]
    public async Task OrderController_Update_ReturnsOkResult()
    {
        // Arrange
        var dto = new OrderDto()
        {
            AddressId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            RestaurantId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.UpdateAsync(dto);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderController_Delete_ReturnsOkResult()
    {
        // Arrange
        var dto = new OrderDto()
        {
            AddressId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            RestaurantId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            OrderStatusId = Guid.NewGuid()
        };

        // Act
        var result = await _controller.Delete(dto.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderController_GetOpenOrder_ReturnsOkResult()
    {
        // Act
        var result = await _controller.GetOpenOrdersForEmployees();

        // Assert
        Assert.IsInstanceOf<List<OrderDto>>(result);
    }
    [Test]
    public async Task OrderController_NumberOfOpenOrders_ReturnsOkResult()
    {
        // Act
        var result = await _controller.NumberOfOpenOrders();

        // Assert
        Assert.IsInstanceOf<int>(result);
    }
    
}