using System.Text;
using APIGateway.Models.PaymentService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIGateway.Controllers;

[Microsoft.AspNetCore.Components.Route("[controller]")]

public class AddressController: ControllerBase
{
    private IHttpClientFactory _factory;
    private HttpClient _client;

    public AddressController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("AddressService");
    }

    [HttpGet("address")]
    public async Task<List<AddressModel>> GetAddress()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Address");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<AddressModel> items = JsonConvert.DeserializeObject<List<AddressModel>>(content);
        return items;
    }
    [HttpGet("address/{id}")]
    public async Task<AddressModel> GetAddress(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Address/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        AddressModel item = JsonConvert.DeserializeObject<AddressModel>(content);
        return item;
    }

    [HttpGet("address/query/{query}")]
    public async Task<List<string>> GetAutoComplete(string query)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Address/Adresser/{query}"); 
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<string> items = JsonConvert.DeserializeObject<List<string>>(content) ?? throw new InvalidOperationException();
        return items;
    }

    [HttpPost("address")]
    public async Task<AddressModel> CreateAddress(string street, string streetNr, string zipCode,string? etage, string? door)
    {
        var json = JsonConvert.SerializeObject(new CreateAddressModel(street,streetNr,zipCode,etage,door));
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/Address/createaddress/{street}/{streetNr}/{zipCode}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        AddressModel item = JsonConvert.DeserializeObject<AddressModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpDelete("address")]
    public async Task<bool> DeleteAddress(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Address/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return answer;
    }


}