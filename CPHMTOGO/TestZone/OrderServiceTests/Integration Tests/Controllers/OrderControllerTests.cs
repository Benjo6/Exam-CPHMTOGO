using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IOrderService>();
        _controller = new OrderController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderDto>()
        {
            new OrderDto {RestaurantId = Guid.NewGuid(),CustomerId = Guid.NewGuid(),Address = Guid.NewGuid(),EmployeeId = Guid.NewGuid(),Id = Guid.NewGuid(),OrderStatusId = Guid.NewGuid()},
            new OrderDto {RestaurantId = Guid.NewGuid(),CustomerId = Guid.NewGuid(),Address = Guid.NewGuid(),EmployeeId = Guid.NewGuid(),Id = Guid.NewGuid(),OrderStatusId = Guid.NewGuid()},
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
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);

        var result = okresult.Value as OrderDto;
        
        Assert.That(result, Is.EqualTo(item));

    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        /*OrderDto? dto = null;
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()

        };
        var orderitems = new List<CreateOrderItemDto>()
        {
            new() { Preference = "",  Price = 500.50, Quantity = 1 },
            new() { Preference = "",  Price = 500.50, Quantity = 2 }
        }; 
        _service.Setup(r => r.CreateOrderTask(It.IsAny<OrderDto>(),It.IsAny<List<CreateOrderItemDto>>()).Result).Returns(dto);

        await _controller.CreateOrder(item.Address,item.CustomerId,item.RestaurantId,orderitems);
        _service.Verify(x=> x.CreateOrderTask(It.IsAny<OrderDto>(),It.IsAny<List<CreateOrderItemDto>>()),Times.Once);
        
        Assert.That(item.Address, Is.EqualTo(dto.Address));
        Assert.That(item.CustomerId, Is.EqualTo(dto.CustomerId));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.EmployeeId, Is.EqualTo(dto.EmployeeId));
        Assert.That(item.RestaurantId, Is.EqualTo(dto.RestaurantId));
        Assert.That(item.OrderStatusId, Is.EqualTo(dto.OrderStatusId));*/

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
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
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
        Assert.That(item.EmployeeId, Is.EqualTo(dto.EmployeeId));

    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        _service.Setup(x => x.Delete(item.Id).Result).Returns(true);

        var okresult =  _controller.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
}