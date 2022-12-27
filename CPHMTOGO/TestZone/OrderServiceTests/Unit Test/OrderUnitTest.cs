using Moq;
using OrderService.Domain;
using OrderService.Repositories.Interfaces;

namespace OrderServiceTests.UnitTest;

public class OrderUnitTest
{
    private Mock<IOrderRepository> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IOrderRepository>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.Create(new Order { AddressId = guid, CustomerId = guid, OrderStatusId = guid, RestaurantId = guid });
        _dot.Object.GetById(guid);
        _dot.Object.Update(new Order{AddressId = guid,CustomerId = guid,OrderStatusId = guid,RestaurantId = guid, Id = guid});
        _dot.Object.SaveChanges();
        _dot.Object.GetAll();
        _dot.Object.GetByCondition(t=>t.AddressId==guid);
        _dot.Object.Delete(guid);
        //Assert
        _dot.VerifyAll();
    }
}