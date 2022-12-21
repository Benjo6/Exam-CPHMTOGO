using Moq;
using PaymentService.Resources;
using PaymentService.Services;
using Stripe;

namespace PaymentServiceTests.Integration_Test.Service;

public class PaymentServiceTests
{
    private IStripeService _service;
    private Mock<TokenService> _tokenService;
    private Mock<CustomerService> _customerService;
    private Mock<PaymentIntentService> _paymentIntentService;
    private Mock<TransferService> _payoutService;


    [SetUp]
    public void Setup()
    {
        _tokenService = new Mock<TokenService>();
        _paymentIntentService = new Mock<PaymentIntentService>();
        _payoutService = new Mock<TransferService>();
        _customerService = new Mock<CustomerService>();
        _service = new StripeService(_tokenService.Object, _customerService.Object,_paymentIntentService.Object, _payoutService.Object);
    }
    
    
    [Test]
    public async Task GetCustomers_ReturnCountOfObjects()
    {
        //Act
        Task<StripeList<Customer>> items = Task.FromResult(new StripeList<Customer>
        {
            Data = new List<Customer>()
            {
                new Customer()
                {
                    Address = new Address(),Balance =50000,CashBalance = new CashBalance(),Currency = "DKK"
                },new Customer()
                {
                    Address = new Address(),Balance =50000,CashBalance = new CashBalance(),Currency = "DKK"
                },new Customer()
                {
                    Address = new Address(),Balance =50000,CashBalance = new CashBalance(),Currency = "DKK"
                }
            }
        });
        
        

        _customerService.Setup(x => x.ListAsync(It.IsAny<CustomerListOptions>(),null,It.IsAny<CancellationToken>())).Returns(items);

        //Act
        var result = await _service.GetCustomers(3, CancellationToken.None);
        Assert.IsNotNull(result);

        //Assert
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result.Count(), Is.Not.EqualTo(4));
        Assert.That(result.Count(), Is.Not.EqualTo(2));
    }
    
    /*[Test]
    public async Task CreateCustomer_ReturnCreatedObject()
    {
        //Arrange
        var item = new CustomerCreateOptions()
        {
            Name = "Name",
            Email = "Name"
        };
        Customer dto = null;

        _customerService.Setup(r => r.CreateAsync(It.IsAny<CustomerCreateOptions>(),It.IsAny<RequestOptions>(),It.IsAny<CancellationToken>())).Callback<Customer>(r=>dto=r);
        

        //Act
        await _service.CreateCustomer(new CreateCustomerResource(item.Email,item.Name,new CreateCardResource(item.Name,item.Email,"string","string","string")),CancellationToken.None);
        _customerService.Verify(x=> x.CreateAsync(It.IsAny<CustomerCreateOptions>(),It.IsAny<RequestOptions>(),It.IsAny<CancellationToken>()),Times.Once);


        //Assert
        Assert.IsNotNull(dto);
        Assert.That(item.Email, Is.EqualTo(dto.Email));
        Assert.That(item.Name, Is.EqualTo(dto.Name));
    }*/
    
   
    
    
}