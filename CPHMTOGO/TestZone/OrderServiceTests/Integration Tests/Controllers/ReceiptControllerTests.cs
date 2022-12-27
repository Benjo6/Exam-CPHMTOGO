using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;
using ILogger = Castle.Core.Logging.ILogger;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class ReceiptControllerTests
{
    private Mock<IReceiptService> _service;
    private ReceiptController _controller;
    private Mock<ILogger<ReceiptController>> _logger;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IReceiptService>();

        // Setup mock methods for IOrderStatusService
        _service.Setup(x => x.Create(It.IsAny<ReceiptDto>())).ReturnsAsync(new ReceiptDto());
        _service.Setup(x => x.GetAll()).ReturnsAsync(new List<ReceiptDto>());
        _service.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new ReceiptDto());
        _service.Setup(x => x.Update(It.IsAny<ReceiptDto>())).ReturnsAsync(new ReceiptDto());
        _service.Setup(x => x.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
        _service.Setup(x => x.GetByOrderId(It.IsAny<Guid>())).ReturnsAsync(new ReceiptDto());

        

        // Assign mock ILogger to _logger
        _logger = new Mock<ILogger<ReceiptController>>();

        // Create instance of ReceiptController with mock IReceiptService and ILogger
        _controller = new ReceiptController(_service.Object, _logger.Object);
    }

    [Test]
    public async Task ReceiptController_Get_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task ReceiptController_GetById_ReturnsOkResult()
    {
        // Act
        var result = await _controller.Get(Guid.NewGuid());

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }

    [Test]
    public async Task ReceiptController_Post_ReturnsOkResult()
    {
        // Arrange
        var dto = new ReceiptDto
        {
            Amount = 40,
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Time = DateTime.Now
        };
        // Act
        var result = await _controller.Post(dto);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderItemController_Update_ReturnsOkResult()
    {
        // Arrange
        var dto = new ReceiptDto
        {
            Amount = 40,
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Time = DateTime.Now
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
        var dto = new ReceiptDto
        {
            Amount = 40,
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Time = DateTime.Now
        };

        // Act
        var result = await _controller.Delete(dto.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task OrderController_GetByOrderId_ReturnsOkResult()
    {
        // Arrange
        var dto = new ReceiptDto
        {
            Amount = 40,
            Id = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Time = DateTime.Now
        };

        // Act
        var result = await _controller.GetByOrderId(dto.Id);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
}