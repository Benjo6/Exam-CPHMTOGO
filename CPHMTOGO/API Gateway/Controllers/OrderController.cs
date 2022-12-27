using Microsoft.AspNetCore.Mvc;
using System.Text;
using APIGateway.Models.OrderService;
using APIGateway.Models.PaymentService;
using APIGateway.Models.UserService;
using GraphQL;
using GraphQL.Client.Abstractions;
using Newtonsoft.Json;

namespace APIGateway.Controllers;
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private readonly HttpClient _client;
    private readonly HttpClient _clientPayment;
    private readonly IGraphQLClient _clientQl;
    private readonly HttpClient _clientUser;
    private readonly HttpClient _clientPaymentLogging;


    public OrderController(IHttpClientFactory factory, IGraphQLClient clientQl)
    {
        _clientQl = clientQl;
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
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There might not be any items in the database");
            
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> items = JsonConvert.DeserializeObject<List<OrderModel>>(content)?? throw new Exception("There is no such receipt in the database");
        return Ok(items);
    } 
    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Order/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"There isn't an order with that {id}");
            
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderModel item = JsonConvert.DeserializeObject<OrderModel>(content)?? throw new Exception("There is no such receipt in the database");
        return Ok(item);
    }

    [HttpGet("order/open-order")]
    public async Task<IActionResult> GetOpenOrdersForEmployees()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/open-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> item = JsonConvert.DeserializeObject<List<OrderModel>>(content)?? throw new Exception("There is no such receipt in the database");
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
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"The order wasn't created in the database");
        }
        response.EnsureSuccessStatusCode();
        string contentOrder = await response.Content.ReadAsStringAsync();
        OrderModel order = JsonConvert.DeserializeObject<OrderModel>(contentOrder) ?? throw new Exception("There is no such receipt in the database");
        
        //Find Receipt
        HttpResponseMessage responseReceipt = await _client.GetAsync($"api/Receipt/order/{order.Id}");
        if (!responseReceipt.IsSuccessStatusCode)
        {
            return Ok($"There is no receipt with the id {order.Id}");
        }
        responseReceipt.EnsureSuccessStatusCode();
        string contentReceipt = await responseReceipt.Content.ReadAsStringAsync();
        OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentReceipt) ?? throw new Exception("There is no such receipt in the database");

        
        //Create PaymentLogging
        var jsonPl = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(order.CustomerId, 
            new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"), receipt.Amount, "CustomerPayment")); 
        var dataPl = new StringContent(jsonPl, Encoding.UTF8, "application/json");
        HttpResponseMessage responsePl = await _clientPaymentLogging.PostAsync("api/PaymentLogging", dataPl);
        if (!responsePl.IsSuccessStatusCode)
        {
            return Ok($"The Paymentlogging isn't created");
        }
        responsePl.EnsureSuccessStatusCode();
        
        //Find Restaurant
        // Construct the GraphQL query
        var query = new GraphQLRequest
        {
            Query = @"query($getRestaurantId: String)  {
  getRestaurant(id: $getRestaurantId) {
    accountId
  }
}",
            Variables = new
            {
                getRestaurantId = order.RestaurantId
            }
        };

        // Execute the query and retrieve the result
        var result = await _clientQl.SendQueryAsync<ResponseRestaurant>(query);
        var restaurant =result.Data.getRestaurant;
        
        //Transfer Money to Restaurant (Change to Account_Id)
        var jsonR = JsonConvert.SerializeObject(new PaymentTransferModel(restaurant.accountId,receipt.Amount));
        var dataR = new StringContent(jsonR, Encoding.UTF8, "application/json");
        HttpResponseMessage responseR = await _clientPayment.PostAsync("stripe/transfermoneytoemployee", dataR);
        if (!responseR.IsSuccessStatusCode)
        {
            return Ok($"The transfer didn't went through");
        }
        responseR.EnsureSuccessStatusCode();

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
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There might not be any items in the database");
            
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderReceiptModel> items = JsonConvert.DeserializeObject<List<OrderReceiptModel>>(content) ?? throw new Exception("It cannot get the items from the list");
        return Ok(items);
    } 
    [HttpGet("receipt/{id}")]
    public async Task<IActionResult> GetReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"There isn't a receipt with that {id}");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("It cannot get the item");
        return Ok(item);
    }
    [HttpGet("receipt/order/{orderId}")]
    public async Task<IActionResult> GetByOrderIdReceipt(Guid orderId)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/order/{orderId}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"There isn't a receipt with that {orderId}");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("It cannot get the item");
        return Ok(item);
    }

    [HttpPut("receipt")]
    public async Task<IActionResult> UpdateReceipt ([FromBody] OrderReceiptModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"api/Receipt", data);
        if (!response.IsSuccessStatusCode)
        {
            return Ok("The Receipt wasn't updated in the database");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }
    
    [HttpDelete("receipt/{id}")]
    public async Task<IActionResult> DeleteReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Receipt/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok(false);
        }
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
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There might not be any items in the database");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderStatusModel> items = JsonConvert.DeserializeObject<List<OrderStatusModel>>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    } 
    [HttpGet("orderstatus/{id}")]
    public async Task<IActionResult> GetOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderStatus/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"There isn't an order status with that {id}");
        }
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
        if (!response.IsSuccessStatusCode)
        {
            return Ok("The OrderStatus isn't updated");
        }
        
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
        if (!response.IsSuccessStatusCode)
        {
            return Ok("OrderStatus didn't successfully update");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        
        //Find Receipt
        HttpResponseMessage responseRec = await _client.GetAsync($"api/Receipt/order/{orderId}");
        if (!responseRec.IsSuccessStatusCode)
        {
            return Ok("Receipt isn't found");
        }
        responseRec.EnsureSuccessStatusCode();
        string contentRec = await responseRec.Content.ReadAsStringAsync();
        OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentRec)?? throw new Exception("It cannot get the item");
        
        //Find Employee
        HttpResponseMessage responseEmployee = await _clientUser.GetAsync($"employee/{employeeId}");
        if (!responseEmployee.IsSuccessStatusCode)
        {
            return Ok("Employee isn't found");
        }
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
        if (!responseEm.IsSuccessStatusCode)
        {
            return Ok("The money wasn't transfered to the employee");
        }
        responseEm.EnsureSuccessStatusCode();
        
        return Ok(item);
    }
    
    [HttpDelete("orderstatus/{id}")]
    public async Task<IActionResult> DeleteOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/OrderStatus/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok(false);
        }

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }

    #endregion


    #region OrderItem
    [HttpGet("orderItem")]
    public async Task<IActionResult> GetOrderItem()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderItem");
        if (!response.IsSuccessStatusCode)
        {
            return Ok("There might not be any items in the database");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderItemModel> items = JsonConvert.DeserializeObject<List<OrderItemModel>>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(items);
    }
    [HttpGet("orderItem/{id}")]
    public async Task<IActionResult> GetOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderItem/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"There isn't an order status with that {id}");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPost("orderItem")]
    public async Task<IActionResult> Post([FromBody] CreateSpecificOrderItemModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/OrderItem", data);
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"The Order Item wasn't created");
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content)?? throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpPut("orderItem")]
    public async Task<IActionResult> UpdateOrderItem (OrderItemModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderItem", data);
        if (!response.IsSuccessStatusCode)
        {
            return Ok($"The Order Item wasn't updated");
        }

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content)?? throw new Exception("There is something wrong with the receiving model");


        return Ok(item);
    }
    
    [HttpDelete("orderItem/{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/OrderItem/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return Ok(false);
        }
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return Ok(answer);
    }
    #endregion

}