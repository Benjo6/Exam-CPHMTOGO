using AutoMapper;
using Moq;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Domain.Dto;
using PaymentLoggingService.Profile;
using PaymentLoggingService.Repositories.Interfaces;
using PaymentLoggingService.Services.Interfaces;

namespace PaymentLoggingServiceTests.Integration_Tests.Services;

public class PaymentLoggingServiceTests
{
    private Mock<IPaymentLoggingRepository> _repository;
    private IPaymentLoggingService _service;
    private IMapper _mapper;


    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<PaymentLoggingProfile>());
        _repository = new Mock<IPaymentLoggingRepository>();
        _mapper = new Mapper(config);
        _service = new PaymentLoggingService.Services.PaymentLoggingService(_repository.Object, _mapper);
    }

    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        //Arrange
        var items = new List<PaymentLogging>()
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
        _repository.Setup(x => x.GetAll()).Returns(items.AsQueryable);
        
        //Act
        var okresult = await _service.GetAll();
        
        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task Get_ReturnObjectById()
    {
        //Arrange
        var item = new PaymentLogging
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        _repository.Setup(x => x.GetById(item.Id).Result).Returns(item);

        //Act
        var okresult = await _service.GetById(item.Id);
        
        //Assert
        Assert.IsNotNull(okresult);
        Assert.That(okresult.Id, Is.EqualTo(item.Id));
        Assert.That(okresult.Amount, Is.EqualTo(item.Amount));
        Assert.That(okresult.From, Is.EqualTo(item.From));
        Assert.That(okresult.To, Is.EqualTo(item.To));
        Assert.That(okresult.Type, Is.EqualTo(item.Type));
    }

    [Test]
    public async Task Create_ReturnCreatedObject()
    {
        //Arrange
        PaymentLogging? dto = null;

        _repository.Setup(r => r.Create(It.IsAny<PaymentLogging>())).Callback<PaymentLogging>(r=>dto=r);
        var item = new PaymentLoggingDto()
        {
            Id = Guid.NewGuid(),
            Amount = 430.50,
            From = Guid.NewGuid(),
            To = Guid.NewGuid(),
            Type = "Payment"
        };
        //Act
        await _service.Create(item);
        _repository.Verify(x=> x.Create(It.IsAny<PaymentLogging>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.From, Is.EqualTo(dto.From));
        Assert.That(item.To, Is.EqualTo(dto.To));
        Assert.That(item.Amount, Is.EqualTo(dto.Amount));
        Assert.That(item.Type, Is.EqualTo(dto.Type));
        Assert.That(item.Id, Is.EqualTo(dto.Id));
    }

}