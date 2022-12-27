using System.Diagnostics;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Domain.Model;
using OrderService.Profile;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class OrderStatusControllerTests
{
    private Mock<IOrderStatusService> _service;
    private OrderStatusController _controller;
    private Mock<ILogger<OrderStatusController>> _logger;



    [SetUp]
    public void Setup()
    {
        _service = new Mock<IOrderStatusService>();

        // Setup mock methods for IOrderStatusService
        _service.Setup(x => x.Create(It.IsAny<OrderStatusDto>())).ReturnsAsync(new OrderStatusDto());
        _service.Setup(x => x.GetAll()).ReturnsAsync(new List<OrderStatusDto>());
        _service.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new OrderStatusDto());
        _service.Setup(x => x.StartOrder(It.IsAny<StartOrderStatusModel>())).ReturnsAsync(new OrderStatusDto());
        _service.Setup(x => x.CloseOrder(It.IsAny<Guid>())).ReturnsAsync(new OrderStatusDto());
        _service.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

        

        // Assign mock ILogger to _logger
        _logger = new Mock<ILogger<OrderStatusController>>();

        // Create instance of OrderStatusController with mock IOrderItemService and ILogger
        _controller = new OrderStatusController(_service.Object, _logger.Object);
    }

    [Test]
    public async Task OrderStatusController_Get_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderStatusController_GetById_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task OrderStatusController_Post_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Post();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderStatusController_StartOrder_ReturnsOkResult()
    {
        // Arrange
        var dto = new StartOrderStatusModel(Guid.NewGuid(), Guid.NewGuid());
        // Act
        var result = await _controller.StartOrder(dto);

        // Assert
        Assert.IsInstanceOf<OrderStatusDto>(result);
    }
    [Test]
    public async Task OrderStatusController_Close_ReturnsOkResult()
    {

        // Act
        var result = await _controller.CloseOrder(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OrderStatusDto>(result);
    }
    [Test]
    public async Task OrderStatusController_Delete_ReturnsOkResult()
    {
        // Arrange
        var dto = new OrderStatusDto();

        // Act
        var result = await _controller.Delete(dto.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}