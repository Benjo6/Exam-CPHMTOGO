using AddressService.Domain;
using AddressService.Repositories.Interfaces;
using Moq;

namespace AddressServiceTests.UnitTest;

public class AddressUnitTest
{ 
    private Mock<IAddressRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IAddressRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new(){Street = "random"});
        _dot.Object.GetById(guid);
        _dot.Object.Update(new () {Street = "random"});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.Street=="random");
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}