using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Internal;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class ReceiptControllerTests
{
    private Mock<IReceiptService> _service;
    private ReceiptController _controller;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IReceiptService>();
        _controller = new ReceiptController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<ReceiptDto>()
        {
            new ReceiptDto { Amount = 500.0, OrderId = Guid.NewGuid(), Time = DateTime.UtcNow },
            new ReceiptDto { Amount = 651.50, OrderId = Guid.NewGuid(), Time = DateTime.UtcNow }
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as List<ReceiptDto>;

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new ReceiptDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };

        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as ReceiptDto;
        
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task CreateReceipt_ReturnCreatedObject()
    {
        //Arrange
        ReceiptDto? dto = null;

        _service.Setup(r => r.Create(It.IsAny<ReceiptDto>()).Result).Callback<ReceiptDto>(r=>dto=r);
        var item = new ReceiptDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        await _controller.Post(item);
        _service.Verify(x=> x.Create(It.IsAny<ReceiptDto>()),Times.Once);


        //Assert
        Assert.That(item.Amount, Is.EqualTo(dto.Amount));
        Assert.That(item.Time, Is.EqualTo(dto.Time));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        ReceiptDto? dto = null;

        _service.Setup(r => r.Update(It.IsAny<ReceiptDto>()).Result).Callback<ReceiptDto>(r=>dto=r);
        var item = new ReceiptDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        await _controller.Put(item);
        _service.Verify(x=> x.Update(It.IsAny<ReceiptDto>()),Times.Once);


        //Assert
        Assert.That(item.Amount, Is.EqualTo(dto.Amount));
        Assert.That(item.Time, Is.EqualTo(dto.Time));
        Assert.That(item.OrderId, Is.EqualTo(dto.OrderId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new ReceiptDto()
        {
            Id = Guid.NewGuid(),
            Amount = 56.65,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        _service.Setup(x => x.Delete(item.Id).Result).Returns(true);

        var okresult =  _controller.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}