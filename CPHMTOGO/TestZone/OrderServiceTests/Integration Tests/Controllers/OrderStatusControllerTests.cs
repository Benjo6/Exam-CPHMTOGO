using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderService.Controllers;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Controllers;

public class OrderStatusControllerTests
{
    private Mock<IOrderStatusService> _service;
    private Mock<IOrderService> _serviceOrder;
    private OrderStatusController _controller;


    [SetUp]
    public void Setup()
    {
        _serviceOrder = new Mock<IOrderService>();
        _service = new Mock<IOrderStatusService>();
        _controller = new OrderStatusController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderStatusDto>()
        {
            new OrderStatusDto {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow},
            new OrderStatusDto {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow}
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as List<OrderStatusDto>;

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new OrderStatusDto 
            {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow};

        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as OrderStatusDto;
        
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        OrderStatusDto? statusDto = null;
        _service.Setup(r => r.Create(It.IsAny<OrderStatusDto>())).Callback<OrderStatusDto>(r => statusDto = r);

        var orderstatus = new OrderStatusDto();

        await _controller.Post();
        _service.Verify(x=> x.Create(It.IsAny<OrderStatusDto>()),Times.Once);
        
        Assert.That(orderstatus.Status, Is.EqualTo(statusDto.Status));
        Assert.That(orderstatus.TimeStamp, Is.EqualTo(statusDto.TimeStamp));
        Assert.That(orderstatus.Id, Is.EqualTo(statusDto.Id));
    }

    [Test]
    public async Task StartOrder_ReturnUpdatedObject()
    {
        //Arrange
        OrderStatusDto dto=null;
        var item= new OrderDto()
        {
            Id = Guid.NewGuid(),
            Address = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            RestaurantId = Guid.NewGuid(),
        };
        _service.Setup(r => r.StartOrder(It.IsAny<Guid>(),It.IsAny<Guid>()).Result).Returns(dto);
        
        //Act
        await _controller.StartOrder(item.Id,item.EmployeeId);
        _service.Verify(x=> x.StartOrder(It.IsAny<Guid>(),It.IsAny<Guid>()),Times.Once);
        
        //Assert
        Assert.That(item.OrderStatusId, Is.EqualTo(dto.Id));
        Assert.That(Status.DELIVERING.ToString(), Is.EqualTo(dto.Status));
    }
    [Test]
    public async Task CloseOrder_ReturnUpdatedObject()
    {
        throw new NotImplementedException(); 
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderStatusDto()
            {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow};
        _service.Setup(x => x.Delete(item.Id).Result).Returns(true);

        var okresult =  _controller.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}