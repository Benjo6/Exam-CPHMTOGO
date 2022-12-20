using System.Text;
using APIGateway.Models.OrderService;
using APIGateway.Models.UserService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIGateway.Controllers;

[Route("[controller]")]
public class UserController: ControllerBase
{
    private HttpClient _client;
    public UserController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("UserService");

    }

    #region Company

    [HttpGet("company/{id}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"company/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpPost("company")]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"company/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPut("company")]
    public async Task<IActionResult> UpdateCompany([FromBody] CompanyModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"company/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpDelete("company/{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"company/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel answer = JsonConvert.DeserializeObject<CompanyModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(answer);
    }    

    #endregion

    #region Customer
    [HttpGet("customer/{id}")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"customer/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpPost("customer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"customer/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPut("customer")]
    public async Task<IActionResult> UpdateCustomer ([FromBody] CustomerModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"customer/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpDelete("customer/{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"customer/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel answer = JsonConvert.DeserializeObject<CustomerModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(answer);
    }
    

    #endregion

    #region Employee
    [HttpGet("employee/{id}")]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"employee/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpPost("employee")]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"employee/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content) ?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPut("employee")]
    public async Task<IActionResult> UpdateEmployee ([FromBody] EmployeeModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"employee/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpDelete("employee/{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"employee/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel answer = JsonConvert.DeserializeObject<EmployeeModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(answer);
    }
    

    #endregion
    
}