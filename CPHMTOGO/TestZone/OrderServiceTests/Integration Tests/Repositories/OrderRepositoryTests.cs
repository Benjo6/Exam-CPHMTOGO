// using Core.Repository;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using OrderService.Domain;
// using OrderService.Infrastructure;
// using OrderService.Repositories;
// using OrderService.Repositories.Interfaces;
//
// namespace OrderServiceTests.Integration_Tests.Repositories;
//
// public class OrderRepositoryTests
// {
//
//     [Test]
//     public void Add_OrderPassed()
//     {
//         //Arrange
//         var guid = Guid.NewGuid();
//         var testObject = new Order(){Address =guid,CustomerId = guid,RestaurantId = guid,OrderStatusId = guid};
//         dbSetMock.Setup(x => x.Create(It.IsAny<Order>())).Returns(testObject);
//         
//         //Act
//         var repository = new OrderRepository(context.Object);
//         repository.Create(testObject);
//         
//         //Assert
//         context.Verify(x=>x.Set<Order>());
//         dbSetMock.Verify(x=>x.Create(It.Is<Order>(y=>y==testObject)));
//
//     }
//
//     [Test]
//     public void Update_OrderPassed()
//     {
//         //Arrange
//         
//         //Act
//         
//         //Assert
//     }
//     
// }