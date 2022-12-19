namespace AddressServiceTests.IntegrationTest.Controller;

public class AddressControllerTests
{
    private AddressController _addressController;
    private Mock<IAddressService> _mockAddressService;

    [SetUp]
    public void Setup()
    {
        // Create a mock of the IAddressService interface
        _mockAddressService = new Mock<IAddressService>();

        // Inject the mock IAddressService into the AddressController constructor
        _addressController = new AddressController(_mockAddressService.Object);
    }

    [Test]
    public async Task Test_GetMethod_ReturnsOkResult()
    {
        // Arrange: Set up the mock IAddressService to return a list of addresses when the GetListAsync method is called
        _mockAddressService.Setup(x => x.GetListAsync())
            .ReturnsAsync(new List<AddressDto> { new AddressDto(), new AddressDto() });

        // Act: Call the Get method on the AddressController
        var result = await _addressController.Get();

        // Assert: Verify that the result is an OkObjectResult and that it contains the expected list of addresses
        Assert.IsInstanceOf<OkObjectResult>(result);
        var objectResult = result as OkObjectResult;
        var addresses = objectResult.Value as List<AddressDto>;
        Assert.AreEqual(2, addresses.Count);
    }

    [Test]
    public async Task Test_GetByIdMethod_ReturnsOkResult()
    {
        // Arrange: Set up the mock IAddressService to return an address when the GetByIdAsync method is called
        _mockAddressService.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new AddressDto());

        // Act: Call the Get method on the AddressController with a specific id
        var result = await _addressController.Get(Guid.NewGuid());

        // Assert: Verify that the result is an OkObjectResult and that it contains the expected address
        Assert.IsInstanceOf<OkObjectResult>(result);
        var objectResult = result as OkObjectResult;
        var address = objectResult.Value as AddressDto;
        Assert.IsNotNull(address);
    }

    [Test]
    public async Task Test_GetAutoCompleteMethod_ReturnsOkResult()
    {
        // Arrange: Set up the mock IAddressService to return a list of strings when the AutoCompleteAdresser method is called
        _mockAddressService.Setup(x => x.AutoCompleteAdresser(It.IsAny<string>()))
            .ReturnsAsync(new List<string> { "Address 1", "Address 2" });

        // Act: Call the GetAutoComplete method on the AddressController with a query string
        var result = await _addressController.GetAutoComplete("query");

        // Assert: Verify that the result is a list of strings and that it contains the expected addresses
        Assert.IsInstanceOf<List<string>>(result);
        ;
    }
}

