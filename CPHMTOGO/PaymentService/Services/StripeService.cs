using PaymentService.Resources;
using Stripe;

namespace PaymentService.Services;

public class StripeService : IStripeService
{
    private readonly TokenService _tokenService;
    private readonly CustomerService _customerService;
    private readonly ChargeService _chargeService;

    public StripeService(TokenService tokenService, CustomerService customerService, ChargeService chargeService)
    {
        _tokenService = tokenService;
        _customerService = customerService;
        _chargeService = chargeService;
    }

    public async Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
    {
        var tokenOptions = new TokenCreateOptions
        {
            Card = new TokenCardOptions
            {
                Name = resource.Card.Name,
                Number = resource.Card.Number,
                ExpYear = resource.Card.ExpiryYear,
                ExpMonth = resource.Card.ExpiryMonth,
                Cvc = resource.Card.CvC
            }
        };
        var token = await _tokenService.CreateAsync(tokenOptions, null, cancellationToken);

        var customerOptions = new CustomerCreateOptions
        {
            Email = resource.Email,
            Name = resource.Name,
            Source = token.Id
        };
        var customer = await _customerService.CreateAsync(customerOptions, null, cancellationToken);

        return new CustomerResource(customer.Id, customer.Email, customer.Name);
    }

    public async Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
    {
        var chargeOptions = new ChargeCreateOptions
        {
            Currency = resource.Currency,
            Amount = resource.Amount,
            ReceiptEmail = resource.ReceiptEmail,
            Customer = resource.CustomerId,
            Description = resource.Description
        };
        var charge = await _chargeService.CreateAsync(chargeOptions, null, cancellationToken);

        return new ChargeResource(charge.Id, charge.Currency, charge.Amount, charge.CustomerId, charge.ReceiptEmail,
            charge.Description);
    }

    public async Task<CustomerResource> GetCustomerByEmail(string email, CancellationToken cancellationToken)
    {
        var stripeCustomers = await _customerService.ListAsync(new CustomerListOptions()
        {
            Email = email
        },null,cancellationToken);
        if (!stripeCustomers.Any())
            return null;

        var stripeCustomer = stripeCustomers.Single();

        var customer = new CustomerResource(stripeCustomer.Id, email, stripeCustomer.Name);

        return customer;
    }

    public async Task<List<CustomerResource>> GetCustomers(int take,CancellationToken cancellationToken)
    {
        var stripeCustomers = await _customerService.ListAsync(new CustomerListOptions()
        {
            Limit = take > 100 ? 100 : take
        },null,cancellationToken);
        return stripeCustomers.Select(x => new CustomerResource(x.Id, x.Email, x.Name)).ToList();
    
    }

    public async Task<CustomerResource> DeleteCustomerByEmail(string email, CancellationToken cancellationToken)
    {
        var stripeCustomers = await _customerService.ListAsync(new CustomerListOptions()
        {
            Email = email
        }, null, cancellationToken);

        var stripeCustomer = await GetCustomerByEmail(email,cancellationToken);
        if (stripeCustomer == null) return null;

        var deletedStripeCustomer = await _customerService.DeleteAsync(stripeCustomer.CustomerId);
        return new CustomerResource(deletedStripeCustomer.Id, deletedStripeCustomer.Email, deletedStripeCustomer.Name);


    }
}