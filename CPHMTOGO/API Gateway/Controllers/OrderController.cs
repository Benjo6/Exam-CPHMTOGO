using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using APIGateway.Models;
using APIGateway.Models.OrderService;
using APIGateway.Models.PaymentService;
using Newtonsoft.Json;

namespace APIGateway.Controllers;
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private IHttpClientFactory _factory;
    private HttpClient _client;
    private HttpClient _clientPayment;


    public OrderController(IHttpClientFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient("OrderService");
        _clientPayment = factory.CreateClient("PaymentService");
    }

    #region Order

    [HttpGet("order")]
    public async Task<List<OrderModel>> GetOrder()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> items = JsonConvert.DeserializeObject<List<OrderModel>>(content);
        return items;
    } 
    [HttpGet("order/{id}")]
    public async Task<OrderModel> GetOrder(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderModel item = JsonConvert.DeserializeObject<OrderModel>(content);
        return item;
    }

    [HttpGet("order/open-order")]
    public async Task<List<OrderModel>> GetOpenOrdersForEmployees()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/open-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> item = JsonConvert.DeserializeObject<List<OrderModel>>(content);
        return item;
    }

    [HttpGet("order/number-order")]
    public async Task<int> NumberOfOpenOrders()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/number-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        int item = JsonConvert.DeserializeObject<int>(content);
        return item;
    }

    [HttpPost("order/createorder")]
    public async Task<OrderModel> Create(Guid address, Guid customerId, Guid restaurantId,
        [FromBody] List<OrderItemModel> orderItems,[FromBody] PaymentCreateCustomerModel paymentModel)
    {
        //Create Order
        var request = new CreateOrderModel(address, customerId, restaurantId, orderItems);
        var json = JsonConvert.SerializeObject(request);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/OrderItem", data);
        response.EnsureSuccessStatusCode();
        string contentOrder = await response.Content.ReadAsStringAsync();
        OrderModel order = JsonConvert.DeserializeObject<OrderModel>(contentOrder) ?? throw new InvalidOperationException();
        
        //Find Receipt
        HttpResponseMessage responseReceipt = await _client.GetAsync($"api/Receipt"); //orderitem
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(content);
        
        
        //Create Customer
        var jsonCustomer = JsonConvert.SerializeObject(paymentModel);
        var dataCustomer = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");
        HttpResponseMessage responseCustomer = await _client.PostAsync("stripe/createcustomer", dataCustomer);
        responseCustomer.EnsureSuccessStatusCode();
        string contentCustomer = await responseCustomer.Content.ReadAsStringAsync();
        PaymentCustomerModel customer = JsonConvert.DeserializeObject<PaymentCustomerModel>(contentCustomer) ?? throw new InvalidOperationException();


        //Create Charge
        var jsonCharge = JsonConvert.SerializeObject(new PaymentCreateChargeModel((long)Convert.ToDouble(receipt.Amount),customer.CustomerId,customer.Email,"Payment is over" ));
        var dataCharge = new StringContent(jsonCharge, Encoding.UTF8, "application/json");
        HttpResponseMessage responseCharge = await _client.PostAsync("stripe/createcharge", dataCharge);
        responseCharge.EnsureSuccessStatusCode();
        string contentCharge = await responseCharge.Content.ReadAsStringAsync();
        PaymentChargeModel charge = JsonConvert.DeserializeObject<PaymentChargeModel>(contentCharge) ?? throw new InvalidOperationException();
        
        //Create PaymentLogging
        var jsonPL = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(order.CustomerId,new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"),Convert.ToDouble(charge.Amount),"CustomerPayment"));
        var dataPL = new StringContent(jsonPL, Encoding.UTF8, "application/json");
        HttpResponseMessage responsePL = await _client.PostAsync("api/PaymentLogging", dataPL);
        responsePL.EnsureSuccessStatusCode();
        
        return order;
    }

    

    [HttpDelete("order")]
    public async Task<bool> DeleteOrder(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Order/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return answer;

    }
    #endregion


    #region Receipt
    [HttpGet("receipt")]
    public async Task<List<OrderReceiptModel>> GetReceipt()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Receipt");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderReceiptModel> items = JsonConvert.DeserializeObject<List<OrderReceiptModel>>(content);
        return items;
    } 
    [HttpGet("receipt/{id}")]
    public async Task<OrderReceiptModel> GetReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/Receipt/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content);
        return item;
    }

    [HttpPut("receipt/{model}")]
    public async Task<OrderReceiptModel> UpdateReceipt ([FromBody] OrderReceiptModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync($"api/Receipt/{model}", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content);
        return item;
    }
    
    [HttpDelete("receipt/{id}")]
    public async Task<bool> DeleteReceipt(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/Receipt/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return answer;
    }
    #endregion


    #region Order Status
    [HttpGet("orderstatus")]
    public async Task<List<OrderStatusModel>> GetOrderStatus()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderStatus");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderStatusModel> items = JsonConvert.DeserializeObject<List<OrderStatusModel>>(content);
        return items;
    } 
    [HttpGet("orderstatus/{id}")]
    public async Task<OrderStatusModel> GetOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderStatus/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content);
        return item;
    }

    [HttpPut("orderstatus/startorder/{orderid}/{employeeId}")]
    public async Task<OrderStatusModel> StartOrder(Guid orderid, Guid employeeId)
    {
        var json = JsonConvert.SerializeObject(new StartOrderStatusModel(orderid,employeeId));
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/startorder",data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content);
        return item;
    }
    [HttpPut("orderstatus/closeorder/{orderid}")]
    public async Task<OrderStatusModel> CloseOrder(Guid orderid)
    {
        var json = JsonConvert.SerializeObject(value: orderid);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/closeorder",data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content);
        return item;
    }
    
    [HttpDelete("orderstatus/{id}")]
    public async Task<bool> DeleteOrderStatus(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"OrderStatus/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return answer;
    }

    #endregion


    #region OrderItem
    [HttpGet("orderitem")]
    public async Task<List<OrderItemModel>> GetOrderItem()
    {
        HttpResponseMessage response = await _client.GetAsync("api/OrderItem");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderItemModel> items = JsonConvert.DeserializeObject<List<OrderItemModel>>(content);
        return items;
    }
    [HttpGet("orderitem/{id}")]
    public async Task<OrderItemModel> GetOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.GetAsync($"api/OrderItem/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content);
        return item;
    }

    [HttpPost("orderitem")]
    public async Task<OrderItemModel> Post([FromBody] CreateOrderItemModel model)
    {
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PostAsync($"api/OrderItem", data);
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content) ?? throw new InvalidOperationException();
        return item;
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
    public async Task<bool> DeleteOrderItem(Guid id)
    {
        HttpResponseMessage response = await _client.DeleteAsync($"api/OrderItem/{id}");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        bool answer = JsonConvert.DeserializeObject<bool>(content);
        return answer;
    }
    #endregion

}