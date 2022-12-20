using APIGateway.Models;
using APIGateway.Models.PaymentService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIGateway.Controllers;
[Route("[controller]")]
public class PaymentLoggingController: ControllerBase
{
    private HttpClient _client;

    public PaymentLoggingController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("PaymentLoggingService");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Paymentlogging") ;
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentLoggingModel> items = JsonConvert.DeserializeObject<List<PaymentLoggingModel>>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get( Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/PaymentLogging/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentLoggingModel item = JsonConvert.DeserializeObject<PaymentLoggingModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
}