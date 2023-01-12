using System.Net;
using System.Text;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Service;
using AddressService.Domain;
using AddressService.Domain.Dto;
using AddressService.Repositories.Interfaces;
using AddressService.Services;
using AddressService.Services.Interfaces;
using AddressService.Stub;
using Newtonsoft.Json.Linq;

namespace AddressService.Tests
{
    [TestFixture]
    public class AddressServiceTests
    {
        private Services.AddressService _addressService;
        private Mock<IAddressRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private MyHttpClientFactoryStub _httpClientFactoryStub;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IAddressRepository>();
            _mockMapper = new Mock<IMapper>();
            _httpClientFactoryStub = new MyHttpClientFactoryStub();
            _addressService =
                new Services.AddressService(_mockRepository.Object, _mockMapper.Object,httpClientFactoryStub: _httpClientFactoryStub);
        }

        [Test]
        public async Task CreateAsync_ValidInput_ReturnsAddressDto()
        {
            // Arrange
            // Configure the mock repository to return an address object when the Create method is called
            Address address = new Address() { Street = "Main St", StreetNr = "1", Zipcode = "12345" };
            _mockRepository.Setup(x => x.Create(It.IsAny<Address>())).Returns(address);

            // Configure the mock mapper to return an AddressDto object when the Map method is called
            AddressDto addressDto = new AddressDto() { Street = "Main St", StreetNr = "1", ZipCode = "12345" };
            _mockMapper.Setup(x => x.Map<Address, AddressDto>(It.IsAny<Address>())).Returns(addressDto);

            // Configure the httpClientFactoryStub to return a successful response when the GetAsync method is called
            _httpClientFactoryStub.SetupGetAsync("https://api.dataforsyningen.dk/adresser?vejnavn=Main St&husnr=1&postnr=12345&etage=&dør=", new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[{'adgangsadresse': { 'adgangspunkt': {'koordinater':[10, 20] } } }]", Encoding.UTF8, "application/json") 
            });
            
            // Act
            var result = await _addressService.CreateAsync("Main St", "1", "12345", null, null);

            // Assert
            Assert.IsInstanceOf<AddressDto>(result);
            Assert.AreEqual(result.Street, "Main St");
            Assert.AreEqual(result.StreetNr, "1");
            Assert.AreEqual(result.ZipCode, "12345");
        }

        [Test]
        public async Task AutoCompleteAdresser_ValidInput_ReturnsListOfString()
        {
            // Arrange
            _httpClientFactoryStub.SetupGetAsync("https://api.dataforsyningen.dk/adresser/autocomplete?query=main", new HttpResponseMessage(HttpStatusCode.OK) 
            {
                Content = new StringContent("[{ 'tekst':'Main Street 1' }, { 'tekst':'Main Street 2' } ]", Encoding.UTF8, "application/json") 
            });

            // Act
            var result = await _addressService.AutoCompleteAdresser("main");

            // Assert
            Assert.IsInstanceOf<List<string>>(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0], "Main Street 1");
            Assert.AreEqual(result[1], "Main Street 2");
        }
    }
}