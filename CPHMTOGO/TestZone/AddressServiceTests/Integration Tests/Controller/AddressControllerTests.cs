
using AddressService.Controllers;
using AddressService.Domain.Dto;
using AddressService.Services.Interfaces;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AddressServiceTests.Integration_Tests.Controller;

public class AddressControllerTests
{
    private AddressController _controller;
    private Mock<IAddressService> _service;
    private Mock<ILogger<AddressController>> _logger;

    [SetUp]
    public void Setup()
    {
        // Create a mock of the IAddressService interface
        _service = new Mock<IAddressService>();
        _logger = new Mock<ILogger<AddressController>>();

            // Inject the mock IAddressService into the AddressController constructor
        _controller = new AddressController(_service.Object,_logger.Object);
    }
    [Test]
    public async Task AddressController_GetAll_ReturnsOkResult()
    {
        //Arrange 
        _service.Setup(x => x.GetAll()).ReturnsAsync(new List<AddressDto>());

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
    }
    [Test]
    public async Task Get_ReturnObjectById()
    {
        //Arrange
        var item = new AddressDto()
        {
            Id = Guid.NewGuid(),
       
        };
        _service.Setup(x => x.GetById(item.Id).Result).Returns(item);
        
        //Act
        var okresult = await _controller.Get(item.Id) as OkObjectResult;
        Assert.IsNotNull(okresult);
        var result = okresult.Value as AddressDto;
        
        //Assert
        Assert.That(result, Is.EqualTo(item));

    }
    
    [Test]
    public void TestGetAutoComplete()
    {
        // Arrange
        var query = "some query";
        var expected = new List<string> {"result 1", "result 2"};

        // Configure the mock IAddressService to return the expected list of strings when the AutoCompleteAdresser method is called
        _service.Setup(service => service.AutoCompleteAdresser(query)).ReturnsAsync(expected);

        // Act
        var result = _controller.GetAutoComplete(query).Result;

        // Assert
        Assert.That(expected, Is.EqualTo(result));
    }

    [Test]
    public void TestCreateAddress()
    {
        // Arrange
        var street = "some street";
        var streetNr = "some street number";
        var zipCode = "some zip code";
        var etage = "some etage";
        var door = "some door";
        var expected = new AddressDto {Id = Guid.NewGuid()};

        // Configure the mock IAddressService to return the expected AddressDto when the CreateAsync method is called with the provided parameters
        _service.Setup(service => service.CreateAsync(street, streetNr, zipCode, etage, door)).ReturnsAsync(expected);

        // Act
        var result = _controller.CreateAddress(street, streetNr, zipCode, etage, door).Result;

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
    
    
    
    

}