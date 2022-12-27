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
    private HttpClient _client;
    public PaymentController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("PaymentService");
    }
    [HttpGet("customer")]
    public async Task<IActionResult> GetCustomer(int take)
    {
        string url = "stripe/customer" + (take < 2 ? "" : "?take=" + take);
        HttpResponseMessage response = await _client.GetAsync(url); 
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentCustomerModel> items = JsonConvert.DeserializeObject<List<PaymentCustomerModel>>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }

    [HttpGet("customer/{email}")]
    public async Task<IActionResult> GetCustomer(string email)
    {
        HttpResponseMessage response = await _client.GetAsync($"stripe/customer/{email}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpGet("charge")]
    public async Task<IActionResult> GetCharges(int take)
    {
        string url = "stripe/charge"+(take<2?"":"?take="+take);
        HttpResponseMessage response = await _client.GetAsync(url); 
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentChargeModel> items = JsonConvert.DeserializeObject<List<PaymentChargeModel>>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }

    [HttpPost("createcustomer")]
    public async Task<IActionResult> CreateCustomer([FromBody] PaymentCreateCustomerModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/createcustomer", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpPost("createcharge")]
    public async Task<IActionResult> CreateCharge([FromBody] PaymentCreateChargeModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/createcharge", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentChargeModel item = JsonConvert.DeserializeObject<PaymentChargeModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpPost("transfermoneytoemployee")]
    public async Task<IActionResult> TransferMoneyToEmployee([FromBody] PaymentTransferModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/transfermoneytoemployee", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentPayoutModel item = JsonConvert.DeserializeObject<PaymentPayoutModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPost("transfermoneytorestaurant")]
    public async Task<IActionResult> TransferMoneyToRestaurant([FromBody]PaymentTransferModel resource)
    {
        var json = JsonConvert.SerializeObject(resource);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync("stripe/transfermoneytorestaurant", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentPayoutModel item = JsonConvert.DeserializeObject<PaymentPayoutModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpDelete("customer")]
    public async Task<IActionResult> DeleteCustomer(string email)
    {
        HttpResponseMessage response = await _client.DeleteAsync("stripe/customer?email="+email);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentCustomerModel item = JsonConvert.DeserializeObject<PaymentCustomerModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);

    }
    



}