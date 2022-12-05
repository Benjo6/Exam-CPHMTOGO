using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Get()
    {
        HttpResponseMessage response = await _client.GetAsync("api/PaymentLogging");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic items = JArray.Parse(content);
        return items;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get( Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/PaymentLogging/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
}