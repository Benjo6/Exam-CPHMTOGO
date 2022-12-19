using AddressService.Domain;
using AddressService.Repositories;
using AddressService.Repositories.Interfaces;
using AutoMapper;
using Moq;

namespace AddressServiceTests.Integration_Tests.Service;
public class AddressServiceTests
    {
        private AddressService.Services.AddressService _addressService;
        private Mock<IAddressRepository> _mockAddressRepository;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockMapper = new Mock<IMapper>();

            // Use the mock implementations when creating a new instance of the AddressService class
            _addressService = new AddressService.Services.AddressService(_mockAddressRepository.Object, _mockMapper.Object,_mockHttpClientFactory.Object);
        }

        [Test]
        public async Task TestCreateAsync()
        {
            // Arrange
            var street = "My Street";
            var streetNr = "123";
            var zipCode = "12345";
            var etage = "1";
            var door = "A";

            // Set up the mocked AddressRepository
            var mockRepository = new Mock<IAddressRepository>();

            // Set up the mocked IMapper
            var mockMapper = new Mock<IMapper>();

            // Set up the mocked IHttpClientFactory
            var mockFactory = new Mock<IHttpClientFactory>();

            // Set up the AddressService with the mocked dependencies
            var service = new AddressService.Services.AddressService(mockRepository.Object, mockMapper.Object, mockFactory.Object);

            // Act
            var result = await service.CreateAsync(street, streetNr, zipCode, etage, door);

            // Assert
            // Add your assertions here
        }
    }