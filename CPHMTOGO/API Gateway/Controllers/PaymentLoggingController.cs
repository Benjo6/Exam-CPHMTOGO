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
    private IHttpClientFactory _factory;
    private HttpClient _client;

    public PaymentLoggingController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("PaymentLoggingService");
    }

    [HttpGet]
    public async Task<List<PaymentLoggingModel>> Get()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Paymentlogging") ;
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<PaymentLoggingModel> items = JsonConvert.DeserializeObject<List<PaymentLoggingModel>>(content);
        return items;
    }

    [HttpGet("{id}")]
    public async Task<PaymentLoggingModel> Get( Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/PaymentLogging/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        PaymentLoggingModel item = JsonConvert.DeserializeObject<PaymentLoggingModel>(content);
        return item;
    }
}