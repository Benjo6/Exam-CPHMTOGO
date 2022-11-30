using AutoMapper;
using Moq;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Services;

public class OrderStatusServiceTests
{
    private Mock<IOrderStatusRepository> _repository;
    private Mock<IOrderRepository> _orderRepository;

    private IOrderStatusService _service;
    private IMapper _mapper;
    


    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>());
        _orderRepository = new Mock<IOrderRepository>();
        _repository = new Mock<IOrderStatusRepository>();
        _mapper = new Mapper(config);
        _service = new OrderStatusService(_repository.Object, _mapper,_orderRepository.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<OrderStatus>()
        {
            new () {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow},
            new () {Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow},

        };
        _repository.Setup(x => x.GetAll()).Returns(items.AsQueryable());

        var okresult = await _service.GetAll();
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new OrderStatus { Status = Status.STARTED.ToString(), TimeStamp = DateTime.UtcNow };


        _repository.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _service.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Status, Is.EqualTo(item.Status));
        Assert.That(okresult.TimeStamp, Is.EqualTo(item.TimeStamp));
        
    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        OrderStatus? dto = null;

        _repository.Setup(r => r.Create(It.IsAny<OrderStatus>())).Callback<OrderStatus>(r=>dto=r);
        var item = new OrderStatusDto { Status = Status.STARTED.ToString(), TimeStamp = DateTime.UtcNow };

        //Act
        await _service.Create(item);
        _repository.Verify(x=> x.Create(It.IsAny<OrderStatus>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.IsNotNull(dto);
        Assert.That(dto.Id, Is.EqualTo(item.Id));
        Assert.That(dto.Status, Is.EqualTo(item.Status));
        Assert.That(dto.TimeStamp, Is.EqualTo(item.TimeStamp));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        OrderStatus? dto = null;

        _repository.Setup(r => r.Update(It.IsAny<OrderStatus>())).Callback<OrderStatus>(r=>dto=r);
        var item = new OrderStatusDto { Status = Status.STARTED.ToString(), TimeStamp = DateTime.UtcNow };

        //Act
        await _service.Update(item);
        _repository.Verify(x=> x.Update(It.IsAny<OrderStatus>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.IsNotNull(dto);
        Assert.That(dto.Id, Is.EqualTo(item.Id));
        Assert.That(dto.Status, Is.EqualTo(item.Status));
        Assert.That(dto.TimeStamp, Is.EqualTo(item.TimeStamp));
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderStatusDto { Status = Status.STARTED.ToString(), TimeStamp = DateTime.UtcNow };

        _repository.Setup(x => x.Delete(item.Id));

        var okresult =  _service.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}