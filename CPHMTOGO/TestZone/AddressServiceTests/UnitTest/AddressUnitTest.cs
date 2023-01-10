using AddressService.Domain;

namespace AddressServiceTests.UnitTest;

[TestFixture]
public class AddressUnitTests
{
    private Address _address;

    [SetUp]
    public void SetUp()
    {
        _address = new Address
        {
            Street = "Test Street",
            StreetNr = "123",
            Zipcode = "12345",
            Longitude = 0,
            Latitude = 0,
            Etage = "3",
            Door = "A"
        };
    }

    [Test]
    public void Address_Street_ShouldBeSet()
    {
        Assert.That(_address.Street, Is.EqualTo("Test Street"));
    }

    [Test]
    public void Address_StreetNr_ShouldBeSet()
    {
        Assert.That(_address.StreetNr, Is.EqualTo("123"));
    }

    [Test]
    public void Address_Zipcode_ShouldBeSet()
    {
        Assert.That(_address.Zipcode, Is.EqualTo("12345"));
    }

    [Test]
    public void Address_Longitude_ShouldBeSet()
    {
        Assert.That(_address.Longitude, Is.EqualTo(0));
    }

    [Test]
    public void Address_Latitude_ShouldBeSet()
    {
        Assert.That(_address.Latitude, Is.EqualTo(0));
    }
        
    [Test]
    public void Address_Etage_ShouldBeSet()
    {
        Assert.That(_address.Etage, Is.EqualTo("3"));
    }
        
    [Test]
    public void Address_Door_ShouldBeSet()
    {
        Assert.That(_address.Door, Is.EqualTo("A"));
    }
}