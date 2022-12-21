using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentService.Controllers;
using PaymentService.Resources;
using PaymentService.Services;

namespace PaymentServiceTests.Integration_Test.Controller;

public class PaymentControllerTests
{
    private Mock<IStripeService> _service;
    private StripeController _controller;


    [SetUp]
    public void Setup()
    {
        _service = new Mock<IStripeService>();
        _controller = new StripeController(_service.Object);
    }

    [Test]
    public async Task GetCustomers_ReturnCountOfObjects()
    {
        //Act
        var items = new List<CustomerResource>()
        {
            new ("customerId","Email","Name"),
            new ("customerId","Email","Name"),
            new ("customerId","Email","Name"),
        };
        _service.Setup(x => x.GetCustomers(3, CancellationToken.None).Result).Returns(items);

        //Act
        var okresult = await _controller.GetCustomers(3, CancellationToken.None);
        Assert.IsNotNull(okresult);
        var list = okresult.Result as OkObjectResult;
        var result = list.Value as List<CustomerResource>;

        //Assert
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result.Count(), Is.Not.EqualTo(4));
        Assert.That(result.Count(), Is.Not.EqualTo(2));
    }

    // [Test]
    // public async Task Get_ReturnObjectByEmail()
    // {
    //     //Act
    //     var item = new CustomerResource("customerId", "Email", "Name");
    //     var item2 = new CustomerResource("customerId2", "Email2", "Name");
    //
    //     _service.Setup(x => x.GetCustomerByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()).Result).Returns(item);
    //
    //     //Act
    //     var okresult = await _controller.GetCustomerByEmail("Email", new CancellationToken());
    //     var r = okresult.Result as dynamic;
    //     Assert.IsNotNull(okresult);
    //     var result = r;
    //
    //     //Assert
    //     Assert.That(result.Email, Is.EqualTo(item.Email));
    //     Assert.That(item2.Email, Is.Not.SameAs(result.Email));
    //
    // }

    [Test]
    public async Task CreateCharge_ReturnCreatedObject()
    {
        //Arrange
        var item = new CreateChargeResource(50000,"string","string","string");
        ChargeResource dto = new ChargeResource("string","DKK",item.Amount,item.CustomerId,item.ReceiptEmail,item.Description);
        var item2 = new CreateChargeResource(70000,"strin","strin","strin");

        _service.Setup(r => r.CreateCharge(It.IsAny<CreateChargeResource>(),CancellationToken.None).Result).Returns(dto);
        

        //Act
        await _controller.CreateCharge(item,CancellationToken.None);
        _service.Verify(x=> x.CreateCharge(It.IsAny<CreateChargeResource>(),CancellationToken.None),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Description, Is.EqualTo(dto.Description));
        Assert.That(item.Amount, Is.EqualTo(dto.Amount));
        Assert.That(item.CustomerId, Is.EqualTo(dto.CustomerId));
        Assert.That(item.ReceiptEmail, Is.EqualTo(dto.ReceiptEmail));
        Assert.That(item2.Amount, Is.Not.EqualTo(dto.Amount));
    }
    
    [Test]
    public async Task CreateCustomer_ReturnCreatedObject()
    {
        //Arrange
        var item = new CreateCustomerResource("Email","Name",new CreateCardResource("Name","Number","ExpiryYear","ExpiryMonth","CvC"));
        CustomerResource dto = new CustomerResource("string",item.Email,item.Name);
        var item2 = new CreateCustomerResource("Email2","Name",new CreateCardResource("Name","Number","ExpiryYear","ExpiryMonth","CvC"));

        _service.Setup(r => r.CreateCustomer(It.IsAny<CreateCustomerResource>(),CancellationToken.None).Result).Returns(dto);
        

        //Act
        await _controller.CreateCustomer(item,CancellationToken.None);
        _service.Verify(x=> x.CreateCustomer(It.IsAny<CreateCustomerResource>(),CancellationToken.None),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Email, Is.EqualTo(dto.Email));
        Assert.That(item.Name, Is.EqualTo(dto.Name));
        Assert.That(item2.Email, Is.Not.EqualTo(dto.Email));
    }
    
    [Test]
    public async Task TransferingMoneyToEmployee_ReturnPayResource()
    {
        //Arrange
        var accountId = "right";
        var amount = 500;
        var fakeamount = 400.50;
        PayoutResource dto = new PayoutResource("DKK",(long)Convert.ToDouble(amount),"string");

        _service.Setup(r => r.TransferingMoneyToEmployee(It.IsAny<CreateTransferResource>(),It.IsAny<CancellationToken>()).Result).Returns(dto);
        

        //Act
        await _controller.TransferMoneyToEmployee(new CreateTransferResource(accountId,amount),CancellationToken.None);
        _service.Verify(x=> x.TransferingMoneyToEmployee(It.IsAny<CreateTransferResource>(),It.IsAny<CancellationToken>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(amount, Is.EqualTo(dto.Amount));
        Assert.That(fakeamount, Is.Not.EqualTo(dto.Amount));
    }
    [Test]
    public async Task TransferingMoneyToRestaurant_ReturnPayResource()
    {
        //Arrange
        var accountId = "right";
        var amount = 500;
        var fakeamount = 400.50;
        PayoutResource dto = new PayoutResource("DKK",(long)Convert.ToDouble(amount),"string");

        _service.Setup(r => r.TransferingMoneyToRestaurant(It.IsAny<CreateTransferResource>(),It.IsAny<CancellationToken>()).Result).Returns(dto);
        

        //Act
        await _controller.TransferMoneyToRestaurant(new CreateTransferResource(accountId,amount),CancellationToken.None);
        _service.Verify(x=> x.TransferingMoneyToRestaurant(It.IsAny<CreateTransferResource>(),It.IsAny<CancellationToken>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(amount, Is.EqualTo(dto.Amount));
        Assert.That(fakeamount, Is.Not.EqualTo(dto.Amount));
    }
    [Test]
    public void Delete_ReturnNull()
    {
        var item = new CustomerResource("customerId", "Email", "Name");


        _service.Setup(x => x.DeleteCustomerByEmail(It.IsAny<string>(),It.IsAny<CancellationToken>()).Result).Returns(It.IsAny<CustomerResource>());

        var okresult =  _controller.DeleteCustomer(item.Email,CancellationToken.None).Result;
        
        Assert.IsNull(okresult.Value);
    }

    
}
