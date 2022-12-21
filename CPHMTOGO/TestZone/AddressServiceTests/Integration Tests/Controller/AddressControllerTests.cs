
using AddressService.Controllers;
using AddressService.Domain.Dto;
using AddressService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AddressServiceTests.Integration_Tests.Controller;

public class AddressControllerTests
{
    private AddressController _controller;
    private Mock<IAddressService> _service;

    [SetUp]
    public void Setup()
    {
        // Create a mock of the IAddressService interface
        _service = new Mock<IAddressService>();

        // Inject the mock IAddressService into the AddressController constructor
        _controller = new AddressController(_service.Object);
    }
    [Test]
    public async Task Get_ReturnCountOfObjects()
    {
        //Act
        var items = new List<AddressDto>()
        {
            new() {
                Id = Guid.NewGuid(),
            },
            new() {
                Id = Guid.NewGuid(),

            },
            new() {
                Id = Guid.NewGuid(),

            }
        };
        _service.Setup(x => x.GetAll().Result).Returns(items);

        //Act
        var okresult = await _controller.Get() as OkObjectResult;
        Assert.IsNotNull(okresult);
        var result = okresult.Value as List<AddressDto>;
        
        //Assert
        Assert.That(result.Count(), Is.EqualTo(3));
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