using Microsoft.AspNetCore.Razor.TagHelpers;
using PaymentService.Resources;
using Stripe;

namespace PaymentService.Services;

public class StripeService : IStripeService
{
    private readonly TokenService _tokenService;
    private readonly CustomerService _customerService;
    private readonly PaymentIntentService _paymentIntentService;
    private readonly TransferService _payoutService;

    public StripeService(TokenService tokenService, CustomerService customerService, PaymentIntentService paymentIntentService, TransferService payoutService) 
    {
        _tokenService = tokenService;
        _customerService = customerService;
        _paymentIntentService = paymentIntentService;
        _payoutService = payoutService;
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
        var chargeOptions = new PaymentIntentCreateOptions
        {
            Currency = "DKK",
            Amount = resource.Amount,
            ReceiptEmail = resource.ReceiptEmail,
            Customer = resource.CustomerId,
            Description = resource.Description
        };
        var charge = await _paymentIntentService.CreateAsync(chargeOptions, null, cancellationToken);

        return new ChargeResource(charge.Id, charge.Currency, charge.Amount, charge.CustomerId, charge.ReceiptEmail,
            charge.Description);
    }

    public async Task<PayoutResource> TransferingMoneyToRestaurant(CreateTransferResource resource,CancellationToken cancellationToken)
    {
        var amount = resource.Amount;
        if (amount >= 750) amount = amount * 0.97;
        else amount =amount* 0.94;
        
        

        var payoutOptions = new TransferCreateOptions
        {
            Currency = "DKK",
            Amount = (long)Convert.ToDouble(amount),
            Description = "Payment for the order",
            Destination = resource.AccountId
        };
        var payout = await _payoutService.CreateAsync(payoutOptions,null, cancellationToken);
        
        return new PayoutResource(payout.Currency, payout.Amount, payout.Description);
    }

    public async Task<PayoutResource> TransferingMoneyToEmployee(CreateTransferResource resource,
        CancellationToken cancellationToken)
    {
        var payoutOptions = new TransferCreateOptions
        {
            Currency = "DKK",
            Amount = (long)Convert.ToDouble(resource.Amount),
            Description = "Payment for the employeee delievering orders",
            Destination = resource.AccountId
        };
        var payout = await _payoutService.CreateAsync(payoutOptions, null, cancellationToken);

        return new PayoutResource(payout.Currency, payout.Amount, payout.Description);
    }

    public async Task<IEnumerable<ChargeResource>> GetCharges(int take, CancellationToken cancellationToken)
    {
        var stripeCharges = await _paymentIntentService.ListAsync(new PaymentIntentListOptions()
        {
            Limit = take > 100 ? 100 : take
        },null,cancellationToken);
        return stripeCharges.Select(x =>
            new ChargeResource(x.Id, x.Currency, x.Amount, x.CustomerId, x.ReceiptEmail, x.Description));
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