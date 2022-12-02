using PaymentService.Resources;
using Stripe;

namespace PaymentService.Services;

public interface IStripeService
{
    Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
    Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);
    Task<CustomerResource> GetCustomerByEmail(string email,CancellationToken cancellationToken); 
    Task<List<CustomerResource>> GetCustomers(int take,CancellationToken cancellationToken);
    Task<CustomerResource> DeleteCustomerByEmail(string email,CancellationToken cancellationToken);

}