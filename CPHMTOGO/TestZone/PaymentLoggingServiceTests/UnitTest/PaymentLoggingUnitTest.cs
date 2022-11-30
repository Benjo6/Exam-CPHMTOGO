using Moq;
using PaymentLoggingService.Domain;
using PaymentLoggingService.Repositories.Interfaces;

namespace PaymentLoggingServiceTests.UnitTest;

public class PaymentLoggingUnitTest
{ 
    private Mock<IPaymentLoggingRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IPaymentLoggingRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new PaymentLogging{ From = guid,To = guid,Amount = 50.0, Type = "Hej"});
        _dot.Object.GetById(guid);
        _dot.Object.Update(new PaymentLogging{ From = guid,To = guid,Amount = 55.0, Type = "Hej"});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.Type=="Hej");
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}