using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace APIGateway.Controllers;

public class OrderController:ControllerBase
{
    private IHttpClientFactory _factory;
    private HttpClient _client;


    public OrderController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("OrderService");
    }

    #region Order

    

    [HttpGet("Order")]
    public async Task<IActionResult> GetOrder()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic items = JArray.Parse(content);
        return items;
    } 
    [HttpGet("Order/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
    #endregion


    #region Receipt
    [HttpGet("Receipt")]
    public async Task<IActionResult> GetReceipt()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Receipt");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic items = JArray.Parse(content);
        return items;
    } 
    [HttpGet("Receipt/{id}")]
    public async Task<IActionResult> GetReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
    #endregion


    #region Order Status
    [HttpGet("OrderStatus")]
    public async Task<IActionResult> GetOrderStatus()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderStatus");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic items = JArray.Parse(content);
        return items;
    } 
    [HttpGet("OrderStatus/{id}")]
    public async Task<IActionResult> GetOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderStatus/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
    #endregion


    #region OrderItem
    [HttpGet("OrderItem")]
    public async Task<IActionResult> GetOrderItem()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderItem");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic items = JArray.Parse(content);
        return items;
    }
    [HttpGet("OrderItem/{id}")]
    public async Task<IActionResult> GetOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderItem/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        dynamic item = JObject.Parse(content);
        return item;
    }
    #endregion

}