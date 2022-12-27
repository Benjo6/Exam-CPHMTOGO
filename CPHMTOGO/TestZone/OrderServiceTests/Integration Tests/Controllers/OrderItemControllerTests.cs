using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    private Mock<ILogger<OrderItemController>> _logger;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IOrderItemService>();

        // Setup mock methods for IOrderItemService
        _service.Setup(x => x.Create(It.IsAny<OrderItemDto>())).ReturnsAsync(new OrderItemDto());
        _service.Setup(x => x.GetAll()).ReturnsAsync(new List<OrderItemDto>());
        _service.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new OrderItemDto());
        _service.Setup(x => x.Update(It.IsAny<OrderItemDto>())).ReturnsAsync(new OrderItemDto());
        _service.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
        

        // Assign mock ILogger to _logger
        _logger = new Mock<ILogger<OrderItemController>>();

        // Create instance of OrderItemController with mock IOrderItemService and ILogger
        _controller = new OrderItemController(_service.Object, _logger.Object);
    }

    [Test]
    public async Task OrderItemController_Get_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderItemController_GetById_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderItemController_Post_ReturnsOkResult()
    {
        // Arrange
        var dto =  new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};

        // Act
        var result = await _controller.Post(dto);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderItemController_Update_ReturnsOkResult()
    {
        // Arrange
        var dto = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};

        // Act
        var result = await _controller.UpdateAsync(dto);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderController_Delete_ReturnsOkResult()
    {
        // Arrange
        var dto = new OrderItemDto()
            { Preference = "",OrderId = Guid.NewGuid(),Price = 500.50,Quantity = 1};

        // Act
        var result = await _controller.Delete(dto.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}