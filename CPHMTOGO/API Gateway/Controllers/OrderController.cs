using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using APIGateway.Models;
using APIGateway.Models.OrderService;
using APIGateway.Models.PaymentService;
using APIGateway.Models.RestaurantService;
using APIGateway.Models.UserService;
using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json;
using TimeSpan = System.TimeSpan;

namespace APIGateway.Controllers;
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private HttpClient _client;
    private HttpClient _clientPayment;
    private IGraphQLClient _clientQL;
    private HttpClient _clientUser;
    private  HttpClient _clientPaymentLogging;


    public OrderController(IHttpClientFactory factory, IGraphQLClient clientQl)
    {
        _clientQL = clientQl;
        _client = factory.CreateClient("OrderService");
        _clientPayment = factory.CreateClient("PaymentService");
        _clientPaymentLogging = factory.CreateClient("PaymentLoggingService");

        _clientUser = factory.CreateClient("UserService");

    }

    #region Order

    [HttpGet("order")]
    public async Task<IActionResult> GetOrder()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> items = JsonConvert.DeserializeObject<List<OrderModel>>(content);
        return Ok(items);
    } 
    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderModel item = JsonConvert.DeserializeObject<OrderModel>(content);
        return Ok(item);
    }

    [HttpGet("order/open-order")]
    public async Task<IActionResult> GetOpenOrdersForEmployees()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/open-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> item = JsonConvert.DeserializeObject<List<OrderModel>>(content);
        return Ok(item);
    }

    [HttpGet("order/number-order")]
    public async Task<IActionResult> NumberOfOpenOrders()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/number-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        int item = JsonConvert.DeserializeObject<int>(content);
        return Ok(item);
    }

    [HttpPost("order/createorder")]
    public async Task<IActionResult> Create( [FromBody] CreateOrderModel model)
    {
        //Create Order
        var request = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, $"application/json");
        HttpResponseMessage response = await _client.PostAsync("api/Order", request);
        response.EnsureSuccessStatusCode();
        string contentOrder = await response.Content.ReadAsStringAsync();
        OrderModel order = JsonConvert.DeserializeObject<OrderModel>(contentOrder) ?? throw new InvalidOperationException("There were failures in the order creation process");
        
        //Find Receipt
        HttpResponseMessage responseReceipt = await _client.GetAsync($"api/Receipt/order/{order.Id}");
        responseReceipt.EnsureSuccessStatusCode();
        string contentReceipt = await responseReceipt.Content.ReadAsStringAsync();
        OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentReceipt) ?? throw new Exception("There is no such receipt in the database");

        
        //Create PaymentLogging
        var jsonPL = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(order.CustomerId, 
            new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"), receipt.Amount, "CustomerPayment")); 
        var dataPL = new StringContent(jsonPL, Encoding.UTF8, "application/json");
        HttpResponseMessage responsePL = await _clientPaymentLogging.PostAsync("api/PaymentLogging", dataPL);
        responsePL.EnsureSuccessStatusCode();
        
        //Find Restaurant
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"
                    query($getRestaurantId: String)  {
getRestaurant(id: $getRestaurantId) {
    id
    name
    address
    cityId
    loginInfoId
    kontoNr
    regNr
    CVR
    role
    menus {
      id
      title
      restaurantId
      menuItems {
        id
        name
        description
        price
        menuId
        foodType
      }
    }
  }
}",
            Variables = new
            {
                getRestaurantId = order.RestaurantId
            }
        };

        // Execute the query and retrieve the result
        var result = await _clientQL.SendQueryAsync<ResponseRestaurant>(query);
        var restaurant =result.Data.getRestaurant;
        
        //Transfer Money to Restaurant (Change to Account_Id)
        var jsonR = JsonConvert.SerializeObject(new PaymentTransferModel("acct_1MBM0IEQFUzeCvJi",receipt.Amount));
        var dataR = new StringContent(jsonR, Encoding.UTF8, "application/json");
        HttpResponseMessage responseR = await _clientPayment.PostAsync("stripe/transfermoneytoemployee", dataR);
        response.EnsureSuccessStatusCode();

        return Ok(order);
    }

    

    [HttpDelete("order")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);

    }
    #endregion


    #region Receipt
    [HttpGet("receipt")]
    public async Task<IActionResult> GetReceipt()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Receipt");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderReceiptModel> items = JsonConvert.DeserializeObject<List<OrderReceiptModel>>(content) ?? throw new Exception("It cannot get the items from the list");
        return Ok(items);
    } 
    [HttpGet("receipt/{id}")]
    public async Task<IActionResult> GetReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("It cannot get the item");
        return Ok(item);
    }
    [HttpGet("receipt/order/{orderid}")]
    public async Task<IActionResult> GetByOrderIdReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("It cannot get the item");
        return Ok(item);
    }

    [HttpPut("receipt/{model}")]
    public async Task<IActionResult> UpdateReceipt ([FromBody] OrderReceiptModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"api/Receipt/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpDelete("receipt/{id}")]
    public async Task<IActionResult> DeleteReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Receipt/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }
    #endregion


    #region Order Status
    [HttpGet("orderstatus")]
    public async Task<IActionResult> GetOrderStatus()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderStatus");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderStatusModel> items = JsonConvert.DeserializeObject<List<OrderStatusModel>>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    } 
    [HttpGet("orderstatus/{id}")]
    public async Task<IActionResult> GetOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderStatus/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPut("orderstatus/startorder/{orderId}/{employeeId}")]
    public async Task<IActionResult> StartOrder(Guid orderId, Guid employeeId)
    {
        var json = JsonConvert.SerializeObject(new StartOrderStatusModel(orderId,employeeId));

        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/startorder",data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    [HttpPut("orderstatus/closeorder/{orderId}")]
    public async Task<IActionResult> CloseOrder(Guid orderId, Guid employeeId)
    {
        var json = JsonConvert.SerializeObject(orderId);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/closeorder",data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        
        //Find Receipt
        HttpResponseMessage responseRec = await _client.GetAsync($"api/Receipt/order/{orderId}");
        responseRec.EnsureSuccessStatusCode();
        string contentRec = await responseRec.Content.ReadAsStringAsync();
        OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentRec)?? throw new Exception("It cannot get the item");
        
        //Find Employee
        HttpResponseMessage responseEmployee = await _clientUser.GetAsync($"employee/{employeeId}");
        responseEmployee.EnsureSuccessStatusCode();
        string contentEmployee = await response.Content.ReadAsStringAsync();
        EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(contentEmployee)?? throw new Exception("There is something wrong with the receiving model");
        //Transfer money to Employee
        var money = receipt.Amount;
        if (item.TimeSpan.Hour >12)
        {
            money *= 0.05;

        }
        else if (item.TimeSpan.Hour>20)
        {
            money *= 0.07;
        }
        else
        {
            money *= 0.02;
        }

        var jsonEm = JsonConvert.SerializeObject(new PaymentTransferModel("acct_1MBLzdCfd0VXBbOf",money));
        var dataEm = new StringContent(jsonEm, Encoding.UTF8, "application/json");
        HttpResponseMessage responseEm = await _clientPayment.PostAsync("stripe/transfermoneytoemployee", dataEm);
        responseEm.EnsureSuccessStatusCode();
        
        return Ok(item);
    }
    
    [HttpDelete("orderstatus/{id}")]
    public async Task<IActionResult> DeleteOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"OrderStatus/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }

    #endregion


    #region OrderItem
    [HttpGet("orderitem")]
    public async Task<IActionResult> GetOrderItem()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderItem");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderItemModel> items = JsonConvert.DeserializeObject<List<OrderItemModel>>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }
    [HttpGet("orderitem/{id}")]
    public async Task<IActionResult> GetOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderItem/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPost("orderitem")]
    public async Task<IActionResult> Post([FromBody] CreateSpecificOrderItemModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/OrderItem", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpPut("orderitem")]
    public async Task<IActionResult> UpdateOrderItem (OrderItemModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderItem", data);
        return Ok(response);
    }
    
    [HttpDelete("orderitem/{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/OrderItem/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }
    #endregion

}