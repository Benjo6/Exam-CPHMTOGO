using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIGateway.Controllers;

[Microsoft.AspNetCore.Components.Route("[controller]")]

public class AddressController: ControllerBase
{
    private readonly HttpClient _client;

    public AddressController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("AddressService");
    }

    [HttpGet("address")]
    public async Task<IActionResult> GetAddress()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Address");
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There is something with the call, which is made");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<AddressModel> items = JsonConvert.DeserializeObject<List<AddressModel>>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }
    [HttpGet("address/{id}")]
    public async Task<IActionResult> GetAddress(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Address/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There is something with the call, which is made");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        AddressModel item = JsonConvert.DeserializeObject<AddressModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpGet("address/query/{query}")]
    public async Task<IActionResult> GetAutoComplete(string query)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Address/Adresser/{query}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There is something with the call, which is made");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<string> items = JsonConvert.DeserializeObject<List<string>>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }

    [HttpPost("address")]
    public async Task<IActionResult> CreateAddress(string street, string streetNr, string zipCode,string? etage, string? door)
    {
        var json = JsonConvert.SerializeObject(new CreateAddressModel(street,streetNr,zipCode,etage,door));
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/Address/createaddress/{street}/{streetNr}/{zipCode}", data);
        if (!response.IsSuccessStatusCode)
        {
            return Ok("The address wasn't created into the database");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        AddressModel item = JsonConvert.DeserializeObject<AddressModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpDelete("address")]
    public async Task<IActionResult> DeleteAddress(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Address/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok(false);
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }


}