using System.Text;
using APIGateway.Models;
using APIGateway.Models.PaymentService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIGateway.Controllers;

[Route("[controller]")]
public class PaymentController:ControllerBase
{
    private IHttpClientFactory _factory;
    private HttpClient _client;
    public PaymentController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("PaymentService");
    }
    [HttpGet("customer")]
    public async Task<List<PaymentCustomerModel>> GetCustomer(int take)
    {
        string url = "stripe/customer" + (take < 2 ? "" : "?take=" + take);
        HttpResponseMessage response = await _client.GetAsync(url); 
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentCustomerModel> items = JsonConvert.DeserializeObject<List<PaymentCustomerModel>>(content) ?? throw new InvalidOperationException();
        return items;
    }

    [HttpGet("customer/{email}")]
    public async Task<PaymentCustomerModel> GetCustomer(string email)
    {
        HttpResponseMessage response = await _client.GetAsync($"stripe/customer/{email}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpGet("charge")]
    public async Task<List<PaymentChargeModel>> GetCharges(int take)
    {
        string url = "stripe/charge"+(take<2?"":"?take="+take);
        HttpResponseMessage response = await _client.GetAsync(url); 
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentChargeModel> items = JsonConvert.DeserializeObject<List<PaymentChargeModel>>(content) ?? throw new InvalidOperationException();
        return items;
    }

    [HttpPost("createcustomer")]
    public async Task<PaymentCustomerModel> CreateCustomer([FromBody] PaymentCreateCustomerModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/createcustomer", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content) ?? throw new InvalidOperationException();
        return item;
    }


    [HttpPost("createcharge")]
    public async Task<ActionResult<PaymentChargeModel>> CreateCharge([FromBody] PaymentCreateChargeModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/createcharge", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentChargeModel item = JsonConvert.DeserializeObject<PaymentChargeModel>(content) ?? throw new InvalidOperationException();
        return item;
    }


    [HttpPost("transfermoneytoemployee")]
    public async Task<ActionResult<PaymentPayoutModel>> TransferMoneyToEmployee([FromBody] PaymentTransferModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/transfermoneytoemployee", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentPayoutModel item = JsonConvert.DeserializeObject<PaymentPayoutModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpPost("transfermoneytorestaurant")]
    public async Task<ActionResult<PaymentPayoutModel>> TransferMoneyToRestaurant([FromBody]PaymentTransferModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/transfermoneytorestaurant", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentPayoutModel item = JsonConvert.DeserializeObject<PaymentPayoutModel>(content) ?? throw new InvalidOperationException();
        return item;
    }


    [HttpDelete("customer")]
    public async Task<ActionResult<PaymentCustomerModel>> DeleteCustomer(string email)
    {
        HttpResponseMessage response = await _client.DeleteAsync("stripe/customer?email="+email);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content) ?? throw new InvalidOperationException();
        return item;

    }
    



}