using Microsoft.AspNetCore.Mvc;
using PaymentService.Resources;
using PaymentService.Services;

namespace PaymentService.Controllers;

[Route("stripe")]
public class StripeController:ControllerBase
{
    private readonly IStripeService _stripeService;

    public StripeController(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    [HttpPost("customer")]
    public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
        CancellationToken cancellationToken)
    {
        var response = await _stripeService.CreateCustomer(resource, cancellationToken);
        return Ok(response);
    }

    [HttpPost("charge")]
    public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource,
        CancellationToken cancellationToken)
    {
        var response = await _stripeService.CreateCharge(resource, cancellationToken);
        return Ok(response);
    }
    [HttpPost("transfermoneytorestaurant")]
    public async Task<ActionResult<PayoutResource>> TransferMoneyToRestaurant(string accountId, double amount,CancellationToken cancellationToken)
    {
        var response = await _stripeService.TransferingMoneyToRestaurant(accountId, amount, cancellationToken);
        return Ok(response);
    }
    [HttpPost("transfermoneytoemployee")]
    public async Task<ActionResult<PayoutResource>> TransferMoneyToEmployee(string accountId, double amount,CancellationToken cancellationToken)
    {
        var response = await _stripeService.TransferingMoneyToEmployee(accountId, amount, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<CustomerResource>> GetCustomerByEmail(string email,CancellationToken cancellationToken)
    {
        var response = await _stripeService.GetCustomerByEmail(email,cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<CustomerResource>> GetCustomers(int take, CancellationToken cancellationToken)
    {
        var response = await _stripeService.GetCustomers(take,cancellationToken);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<ActionResult<CustomerResource>> DeleteCustomer(string email, CancellationToken cancellationToken)
    {
        var response = await _stripeService.DeleteCustomerByEmail(email, cancellationToken);
        return Ok(response);
    }
}