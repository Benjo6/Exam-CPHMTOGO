using Microsoft.AspNetCore.Mvc;
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
    [HttpGet]
    public async Task<IActionResult> Get(int take)
    {
        HttpResponseMessage response = await _client.GetAsync($"stripe?take={take}"); //Default is 1
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> Get(string email)
    {
        HttpResponseMessage response = await _client.GetAsync($"stripe/{email}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
    
}