using System.Text;
using APIGateway.Models.OrderService;
using APIGateway.Models.UserService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIGateway.Controllers;

[Route("[controller]")]
public class UserController: ControllerBase
{
    private IHttpClientFactory _factory;
    private HttpClient _client;



    public UserController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("UserService");

    }

    #region Company

    [HttpGet("company/{id}")]
    public async Task<CompanyModel> GetCompany(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"company/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content);
        return item;
    }
    
    [HttpPost("company")]
    public async Task<CompanyModel> CreateCompany([FromBody] CompanyModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"company/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpPut("company")]
    public async Task<CompanyModel> UpdateCompany([FromBody] CompanyModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"company/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel item = JsonConvert.DeserializeObject<CompanyModel>(content);
        return item;
    }
    
    [HttpDelete("company/{id}")]
    public async Task<CompanyModel> DeleteCompany(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"company/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CompanyModel answer = JsonConvert.DeserializeObject<CompanyModel>(content);
        return answer;
    }    

    #endregion

    #region Customer
    [HttpGet("customer/{id}")]
    public async Task<CustomerModel> GetCustomer(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"customer/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content);
        return item;
    }
    
    [HttpPost("customer")]
    public async Task<CustomerModel> CreateCustomer([FromBody] CustomerModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"customer/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpPut("customer")]
    public async Task<CustomerModel> UpdateCustomer ([FromBody] CustomerModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"customer/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel item = JsonConvert.DeserializeObject<CustomerModel>(content);
        return item;
    }
    
    [HttpDelete("customer/{id}")]
    public async Task<CustomerModel> DeleteCustomer(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"customer/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        CustomerModel answer = JsonConvert.DeserializeObject<CustomerModel>(content);
        return answer;
    }
    

    #endregion

    #region Employee
    [HttpGet("employee/{id}")]
    public async Task<EmployeeModel> GetEmployee(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"employee/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content);
        return item;
    }
    
    [HttpPost("employee")]
    public async Task<EmployeeModel> CreateEmployee([FromBody] EmployeeModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"employee/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content) ?? throw new InvalidOperationException();
        return item;
    }

    [HttpPut("employee")]
    public async Task<EmployeeModel> UpdateEmployee ([FromBody] EmployeeModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"employee/{model}/{model.Id}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel item = JsonConvert.DeserializeObject<EmployeeModel>(content);
        return item;
    }
    
    [HttpDelete("employee/{id}")]
    public async Task<EmployeeModel> DeleteEmployee(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"employee/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        EmployeeModel answer = JsonConvert.DeserializeObject<EmployeeModel>(content);
        return answer;
    }
    

    #endregion
    
}