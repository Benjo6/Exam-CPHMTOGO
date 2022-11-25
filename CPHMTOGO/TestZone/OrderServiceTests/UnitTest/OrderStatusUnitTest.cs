using Moq;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.UnitTest;

public class OrderStatusUnitTest
{
    private Mock<IOrderStatusRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IOrderStatusRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new OrderStatus{Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow});
        _dot.Object.GetById(guid);
        _dot.Object.Update(new OrderStatus{Status = Status.STARTED.ToString(),TimeStamp = DateTime.UtcNow,Id = guid});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.Status==Status.STARTED.ToString());
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}