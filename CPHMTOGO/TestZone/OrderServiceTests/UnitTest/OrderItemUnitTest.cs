using Moq;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.UnitTest;

public class OrderItemUnitTest
{
    private Mock<IOrderItemRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IOrderItemRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new OrderItem{OrderId = guid,Preference = "Food",Price = 32.05,Quantity = 3});
        _dot.Object.GetById(guid);
        _dot.Object.Update(new OrderItem{Id = guid,OrderId = guid,Preference = "Food",Price = 55.0,Quantity = 3});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.Preference=="Food");
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}