using Moq;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.UnitTest;

public class ReceiptUnitTest
{
    private Mock<IReceiptRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IReceiptRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new Receipt{Amount = 50.50,OrderId = guid,Time = DateTime.UtcNow});
        _dot.Object.GetById(guid);
        _dot.Object.Update(new Receipt{Amount = 70.53,OrderId = guid,Time = DateTime.UtcNow});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.Amount==50.50);
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}