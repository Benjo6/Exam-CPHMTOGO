using AutoMapper;
using Moq;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Services;

public class OrderServiceTests
{
    private Mock<IOrderRepository> _repository;
    private Mock<IOrderStatusService> _statusService;
    private Mock<IOrderItemRepository> _itemRepository;
    private Mock<IReceiptRepository> _receiptRepository;

    private IOrderService _service;
    private IMapper _mapper;


    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>());
        _statusService = new Mock<IOrderStatusService>();
        _itemRepository = new Mock<IOrderItemRepository>();
        _receiptRepository = new Mock<IReceiptRepository>();
        _repository = new Mock<IOrderRepository>();
        _mapper = new Mapper(config);
        _service = new OrderService.Services.OrderService(_repository.Object, _mapper,_statusService.Object,_receiptRepository.Object,_itemRepository.Object);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<Order>()
        {
            new() 
            {
                RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
                EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
            },
            new ()
            {
                RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
                EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
            }
        };
        _repository.Setup(x => x.GetAll()).Returns(items.AsQueryable());

        var okresult = await _service.GetAll();
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new Order
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };

        _repository.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _service.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Address, Is.EqualTo(item.Address));
        Assert.That(okresult.CustomerId, Is.EqualTo(item.CustomerId));
        Assert.That(okresult.EmployeeId, Is.EqualTo(item.EmployeeId));
        Assert.That(okresult.RestaurantId, Is.EqualTo(item.RestaurantId));
        Assert.That(okresult.OrderStatusId, Is.EqualTo(item.OrderStatusId));


    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        Order? dto = null;

        _repository.Setup(r => r.Create(It.IsAny<Order>())).Callback<Order>(r=>dto=r);
        var item = new OrderDto()
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };
        //Act
        await _service.Create(item);
        _repository.Verify(x=> x.Create(It.IsAny<Order>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.Address, Is.EqualTo(dto.Address));
        Assert.That(item.CustomerId, Is.EqualTo(dto.CustomerId));
        Assert.That(item.EmployeeId, Is.EqualTo(dto.EmployeeId));
        Assert.That(item.RestaurantId, Is.EqualTo(dto.RestaurantId));
        Assert.That(item.OrderStatusId, Is.EqualTo(dto.OrderStatusId));
    }

    [Test]
    public async Task Update_ReturnUpdatedObject()
    {
        //Arrange
        Order? dto = null;

        _repository.Setup(r => r.Update(It.IsAny<Order>())).Callback<Order>(r=>dto=r);
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };
        //Act
        await _service.Update(item);
        _repository.Verify(x=> x.Update(It.IsAny<Order>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Id, Is.EqualTo(dto.Id));
        Assert.That(item.Address, Is.EqualTo(dto.Address));
        Assert.That(item.CustomerId, Is.EqualTo(dto.CustomerId));
        Assert.That(item.EmployeeId, Is.EqualTo(dto.EmployeeId));
        Assert.That(item.RestaurantId, Is.EqualTo(dto.RestaurantId));
        Assert.That(item.OrderStatusId, Is.EqualTo(dto.OrderStatusId));
        
    }

    [Test]
    public void Delete_ReturnTrue()
    {
        var item = new OrderDto
        {
            RestaurantId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), Address = Guid.NewGuid(),
            EmployeeId = Guid.NewGuid(), Id = Guid.NewGuid(), OrderStatusId = Guid.NewGuid()
        };
        _repository.Setup(x => x.Delete(item.Id));

        var okresult =  _service.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}