using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentLoggingService.Controllers;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;
using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingServiceTests.Integration_Tests.Controllers;

public class PaymentLoggingControllerTests
{
    private Mock<IPaymentLoggingService> _service;
    private PaymentLoggingController _controller;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IPaymentLoggingService>();
        _controller = new PaymentLoggingController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        //Act
        var items = new List<PaymentLoggingDto>()
        {
            new() {
                Id = Guid.NewGuid(),
                Amount = 760.50,
                From = Guid.NewGuid(),
                To = Guid.NewGuid(),
                Type = "Payment"
            },
            new() {
                Id = Guid.NewGuid(),
                Amount = 250.0,
                From = Guid.NewGuid(),
                To = Guid.NewGuid(),
                Type = "Payment"
            },
            new() {
                Id = Guid.NewGuid(),
                Amount = 430.50,
                From = Guid.NewGuid(),
                To = Guid.NewGuid(),
                Type = "Payment"
            }
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        //Act
        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);
        var result = okresult.Value as List<PaymentLoggingDto>;
        
        //Assert
        Assert.That(result.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {
        //Arrange
        var item = new PaymentLoggingDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);
        
        //Act
        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);
        var result = okresult.Value as PaymentLoggingDto;
        
        //Assert
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task Create_ReturnCreatedObject()
    {
        //Arrange
        PaymentLoggingDto? dto = null;

        _service.Setup(r => r.Create(It.IsAny<PaymentLoggingDto>()).Result).Callback<PaymentLoggingDto>(r=>dto=r);
        var item = new PaymentLoggingDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        //Act
        await _controller.Post(item);
        _service.Verify(x=> x.Create(It.IsAny<PaymentLoggingDto>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.Amount, Is.EqualTo(dto.Amount));
        Assert.That(item.From, Is.EqualTo(dto.From));
        Assert.That(item.To, Is.EqualTo(dto.To));
        Assert.That(item.Type, Is.EqualTo(dto.Type));
    }

    
}