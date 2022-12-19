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

    [HttpPost("createcustomer")]
    public async Task<ActionResult<CustomerResource>> CreateCustomer([FromBody] CreateCustomerResource resource,
        CancellationToken cancellationToken)
    {
        var response = await _stripeService.CreateCustomer(resource, cancellationToken);
        return Ok(response);
    }

    [HttpPost("createcharge")]
    public async Task<ActionResult<ChargeResource>> CreateCharge([FromBody] CreateChargeResource resource,
        CancellationToken cancellationToken)
    {
        var response = await _stripeService.CreateCharge(resource, cancellationToken);
        return Ok(response);
    }
    [HttpPost("transfermoneytorestaurant")]
    public async Task<ActionResult<PayoutResource>> TransferMoneyToRestaurant([FromBody] CreateTransferResource resource,CancellationToken cancellationToken)
    {
        var response = await _stripeService.TransferingMoneyToRestaurant(resource, cancellationToken);
        return Ok(response);
    }
    [HttpPost("transfermoneytoemployee")]
    public async Task<ActionResult<PayoutResource>> TransferMoneyToEmployee([FromBody] CreateTransferResource resource,CancellationToken cancellationToken)
    {
        var response = await _stripeService.TransferingMoneyToEmployee(resource, cancellationToken);
        return Ok(response);
    }

    [HttpGet("customer/{email}")]
    public async Task<ActionResult<CustomerResource>> GetCustomerByEmail(string email,CancellationToken cancellationToken)
    {
        var response = await _stripeService.GetCustomerByEmail(email,cancellationToken);
        return Ok(response);
    }

    [HttpGet("customer")]
    public async Task<ActionResult<List<CustomerResource>>> GetCustomers(int take, CancellationToken cancellationToken)
    {
        var response = await _stripeService.GetCustomers(take,cancellationToken);
        return Ok(response);
    }
    
    [HttpGet("charge")]
    public async Task<ActionResult<List<CustomerResource>>> GetCharges(int take, CancellationToken cancellationToken)
    {
        var response = await _stripeService.GetCharges(take,cancellationToken);
        return Ok(response);
    }

    [HttpDelete("customer")]
    public async Task<ActionResult<CustomerResource>> DeleteCustomer(string email, CancellationToken cancellationToken)
    {
        var response = await _stripeService.DeleteCustomerByEmail(email, cancellationToken);
        return Ok(response);
    }
}