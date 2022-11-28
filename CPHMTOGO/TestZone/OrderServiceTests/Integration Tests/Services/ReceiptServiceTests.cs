using AutoMapper;
using Moq;
using OrderService.Domain;
using OrderService.Domain.Dto;
using OrderService.Profile;
using OrderService.Repositories.Interfaces;
using OrderService.Services;
using OrderService.Services.Interfaces;

namespace OrderServiceTests.Integration_Tests.Services;

public class ReceiptServiceTests
{
    private Mock<IReceiptRepository> _repository;
    private IReceiptService _service;
    private IMapper _mapper;


    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<OrderProfile>());
        _repository = new Mock<IReceiptRepository>();
        _mapper = new Mapper(config);
        _service = new ReceiptService(_repository.Object, _mapper);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        var items = new List<Receipt>()
        {
            new() { Amount = 500.0, OrderId = Guid.NewGuid(), Time = DateTime.UtcNow },
            new() { Amount = 651.50, OrderId = Guid.NewGuid(), Time = DateTime.UtcNow }
        };
        _repository.Setup(x => x.GetAll()).Returns(items.AsQueryable);

        var okresult = await _service.GetAll();
        Assert.IsNotNull(okresult);

        Assert.That(okresult.Count(), Is.EqualTo(2));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {

        var item = new Receipt
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };

        _repository.Setup(x => x.GetById(item.Id).Result).Returns(item);

        var okresult = await _service.GetById(item.Id);
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.Time, Is.EqualTo(item.Time));
        Assert.That(okresult.OrderId, Is.EqualTo(item.OrderId));


    }

    [Test]
    public async Task CreateOrder_ReturnCreatedObject()
    {
        //Arrange
        Receipt? dto = null;

        _repository.Setup(r => r.Create(It.IsAny<Receipt>())).Callback<Receipt>(r=>dto=r);
        var item = new ReceiptDto()
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        await _service.Create(item);
        _repository.Verify(x=> x.Create(It.IsAny<Receipt>()),Times.Once);


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
        Receipt? dto = null;

        _repository.Setup(r => r.Update(It.IsAny<Receipt>())).Callback<Receipt>(r=>dto=r);
        var item = new ReceiptDto
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            OrderId = Guid.NewGuid(),
            Time = DateTime.UtcNow
        };
        //Act
        await _service.Update(item);
        _repository.Verify(x=> x.Update(It.IsAny<Receipt>()),Times.Once);


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
        _repository.Setup(x => x.Delete(item.Id));

        var okresult =  _service.Delete(item.Id).Result;
        
        Assert.IsTrue(okresult);
    }
    
}