using Moq;
using PaymentService.Resources;
using PaymentService.Services;

namespace PaymentServiceTests.Unit_Test;

public class PaymentUnitTest
{
    private Mock<IStripeService> _dot;

    [SetUp]
    public void Setup()
    {
        _dot = new Mock<IStripeService>();
    }

    [Test]
    public void MustCallRepositoryInAllMethods()
    {
        //Arrange
        var guid = Guid.NewGuid();
        //Act
        _dot.Object.CreateCharge(new CreateChargeResource(40000,"string","string","string"),CancellationToken.None);
        _dot.Object.CreateCustomer(new CreateCustomerResource("string","string",new CreateCardResource("string","string","string","string", "string")),CancellationToken.None);
        _dot.Object.GetCustomers(5,CancellationToken.None);
        _dot.Object.DeleteCustomerByEmail("string",CancellationToken.None);
        _dot.Object.TransferingMoneyToEmployee(new CreateTransferResource("accountId",500.00), CancellationToken.None);
        _dot.Object.TransferingMoneyToRestaurant(new CreateTransferResource("accountId",500.00), CancellationToken.None);
        //Assert
        _dot.VerifyAll();
    }
}