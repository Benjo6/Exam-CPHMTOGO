using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Transactions;
using APIGateway.MessagingGateway;
using APIGateway.Models.OrderService;
using APIGateway.Models.PaymentService;
using APIGateway.Models.UserService;
using GraphQL;
using GraphQL.Client.Abstractions;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Exception = System.Exception;
using Message = Experimental.System.Messaging.Message;
using StringContent = System.Net.Http.StringContent;

namespace APIGateway.Controllers;

[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly HttpClient _client;
    private readonly HttpClient _clientPayment;
    private readonly IGraphQLClient _clientQl;
    private readonly HttpClient _clientUser;

    private readonly HttpClient _clientPaymentLogging;

    
    private readonly IConnectionFactory _connectionFactory;
    private readonly ILogger<OrderController> _logger;
    private MessageGateway _messageGateway;

    public OrderController(IHttpClientFactory factory, IGraphQLClient clientQl, ILogger<OrderController> logger,
        IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _messageGateway = new MessageGateway(_connectionFactory);
        _clientQl = clientQl;
        _logger = logger;
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
        List<OrderModel> items = JsonConvert.DeserializeObject<List<OrderModel>>(content) ??
                                 throw new Exception("There is no such receipt in the database");
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
        OrderModel item = JsonConvert.DeserializeObject<OrderModel>(content) ??
                          throw new Exception("There is no such receipt in the database");
        return Ok(item);
    }

    [HttpGet("order/open-order")]
    public async Task<IActionResult> GetOpenOrdersForEmployees()
    {
        HttpResponseMessage response = await _client.GetAsync("api/Order/open-order");
        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        List<OrderModel> item = JsonConvert.DeserializeObject<List<OrderModel>>(content) ??
                                throw new Exception("There is no such receipt in the database");
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
    public async Task<IActionResult> Create([FromBody] CreateOrderModel model)
    {
        // Track the start time of the process
        var startTime = DateTime.Now;
        // Initialize counters for success and error rates
        var successCount = 0;
        var errorCount = 0;

        try
        {
            //Create Order
            var request = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, $"application/json");
            HttpResponseMessage response = await _client.PostAsync("api/Order", request);
            if (!response.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"The order wasn't created in the database");
            }

            successCount++;
            string contentOrder = await response.Content.ReadAsStringAsync();
            OrderModel order = JsonConvert.DeserializeObject<OrderModel>(contentOrder) ??
                               throw new Exception("There is no such receipt in the database");

            //Find Receipt
            HttpResponseMessage responseReceipt = await _client.GetAsync($"api/Receipt/order/{order.Id}");
            if (!responseReceipt.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"There is no receipt with the id {order.Id}");
            }

            successCount++;
            string contentReceipt = await responseReceipt.Content.ReadAsStringAsync();
            OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentReceipt) ??
                                        throw new Exception("There is no such receipt in the database");


            //Create PaymentLogging
            var jsonPl = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(order.CustomerId,
                new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"), receipt.Amount, "CustomerPayment"));
            var dataPl = new StringContent(jsonPl, Encoding.UTF8, "application/json");
            HttpResponseMessage responsePl = await _clientPaymentLogging.PostAsync("api/PaymentLogging", dataPl);
            if (!responsePl.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"The Paymentlogging isn't created");
            }

            successCount++;

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
            if (result == null)
            {
                errorCount++;
            }

            var restaurant = result.Data.getRestaurant;
            successCount++;

            //Transfer Money to Restaurant (Change to Account_Id)
            var jsonR = JsonConvert.SerializeObject(new PaymentTransferModel(restaurant.accountId, receipt.Amount));
            var dataR = new StringContent(jsonR, Encoding.UTF8, "application/json");
            HttpResponseMessage responseR = await _clientPayment.PostAsync("stripe/transfermoneytoemployee", dataR);
            if (!responseR.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"The transfer didn't went through");
            }

            successCount++;

            //Create PaymentLogging for Restaurant payment
            var jsonRe = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(
                new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"),
                order.RestaurantId, receipt.Amount, "PaymentToRestaurant"));
            var dataRe = new StringContent(jsonRe, Encoding.UTF8, "application/json");
            HttpResponseMessage responseRe = await _clientPaymentLogging.PostAsync("api/PaymentLogging", dataRe);
            if (!responseRe.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"The Paymentlogging isn't created");
            }

            successCount++;
            //Find Customer
            HttpResponseMessage responseCustomer = await _clientUser.GetAsync($"customer/{order.CustomerId}");
            if (!responseCustomer.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok("Customer isn't found");
            }

            successCount++;
            string contentCustomer = await responseCustomer.Content.ReadAsStringAsync();
            CustomerModel customer = JsonConvert.DeserializeObject<CustomerModel>(contentCustomer)?? throw new Exception("There is something wrong with the receiving model");

            
            //Message to Customer
            _messageGateway.SendMailMessage("BenjoCh@proton.me",restaurant.name, customer.firstname, customer.lastname,
                receipt.Amount, model.OrderItems);
            

            //Message to Restaurant
            _messageGateway.SendMailMessage("BenjoCh@proton.me", restaurant.name, receipt.Amount, model.OrderItems);
        }
        catch (Exception ex)
        {
            errorCount++;
            _logger.LogError(ex, "An error occurred while processing the order");
        }
        finally
        {
            // Calculate the duration of the process
            var duration = DateTime.Now - startTime;

            // Log the success and error rates
            _logger.LogInformation(
                "Order processing complete. Success rate: {successRate}%, Error rate: {errorRate}%",
                successCount / (successCount + errorCount) * 100, errorCount / (successCount + errorCount) * 100);

            // Log the duration of the process
            _logger.LogInformation("Process duration: {duration}", duration);
        }

        return Ok("The order has been created");
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
        List<OrderReceiptModel> items = JsonConvert.DeserializeObject<List<OrderReceiptModel>>(content) ??
                                        throw new Exception("It cannot get the items from the list");
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
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content) ??
                                 throw new Exception("It cannot get the item");
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
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content) ??
                                 throw new Exception("It cannot get the item");
        return Ok(item);
    }

    [HttpPut("receipt")]
    public async Task<IActionResult> UpdateReceipt([FromBody] OrderReceiptModel model)
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
        OrderReceiptModel item = JsonConvert.DeserializeObject<OrderReceiptModel>(content) ??
                                 throw new Exception("There is something wrong with the receiving model");
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
        List<OrderStatusModel> items = JsonConvert.DeserializeObject<List<OrderStatusModel>>(content) ??
                                       throw new Exception("There is something wrong with the receiving model");
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
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content) ??
                                throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }

    [HttpPut("orderstatus/startorder/{orderId}/{employeeId}")]
    public async Task<IActionResult> StartOrder(Guid orderId, Guid employeeId)
    {
        var json = JsonConvert.SerializeObject(new StartOrderStatusModel(orderId, employeeId));
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/startorder", data);
        if (!response.IsSuccessStatusCode)
        {
            return Ok("The OrderStatus isn't updated");
        }

        response.EnsureSuccessStatusCode();
        string content = await response.Content.ReadAsStringAsync();
        OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content) ??
                                throw new Exception("There is something wrong with the receiving model");
        
        return Ok(item);
    }

    [HttpPut("orderstatus/closeorder/{orderId}")]
    public async Task<IActionResult> CloseOrder(Guid orderId, Guid employeeId)
    {
        // Track the start time of the process
        var startTime = DateTime.Now;

        // Initialize counters for success and error rates
        var successCount = 0;
        var errorCount = 0;
        try
        {
            var json = JsonConvert.SerializeObject(orderId);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync("api/OrderStatus/closeorder", data);
            if (!response.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok("OrderStatus didn't successfully update");
            }

            successCount++;
            string content = await response.Content.ReadAsStringAsync();
            OrderStatusModel item = JsonConvert.DeserializeObject<OrderStatusModel>(content) ??
                                    throw new Exception("There is something wrong with the receiving model");

            //Find Receipt
            HttpResponseMessage responseRec = await _client.GetAsync($"api/Receipt/order/{orderId}");
            if (!responseRec.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok("Receipt isn't found");
            }

            successCount++;
            string contentRec = await responseRec.Content.ReadAsStringAsync();
            OrderReceiptModel receipt = JsonConvert.DeserializeObject<OrderReceiptModel>(contentRec) ??
                                        throw new Exception("It cannot get the item");

            //Find Employee
            HttpResponseMessage responseEmployee = await _clientUser.GetAsync($"employee/{employeeId}");
            if (!responseEmployee.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok("Employee isn't found");
            }

            successCount++;
            string contentEmployee = await responseEmployee.Content.ReadAsStringAsync();
            EmployeeModel employee = JsonConvert.DeserializeObject<EmployeeModel>(contentEmployee) ??
                                     throw new Exception("There is something wrong with the receiving model");

            //Transfer money to Employee
            var money = receipt.Amount;
            if (item.TimeSpan.Hour > 12)
            {
                money *= 0.01;
            }
            else if (item.TimeSpan.Hour > 20)
            {
                money *= 0.015;
            }
            else
            {
                money *= 0.005;
            }

            var jsonEm = JsonConvert.SerializeObject(new PaymentTransferModel(employee.accountId, money));
            var dataEm = new StringContent(jsonEm, Encoding.UTF8, "application/json");
            HttpResponseMessage responseEm = await _clientPayment.PostAsync("stripe/transfermoneytoemployee", dataEm);
            if (!responseEm.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok("The money wasn't transfered to the employee");
            }

            successCount++;
            //Create PaymentLogging for Employee payment
            var jsonRe = JsonConvert.SerializeObject(new CreatePaymentLoggingModel(
                new Guid("cd863618-5ad2-48e6-8d8e-826257160e6d"),
                employeeId, receipt.Amount, "PaymentToEmployee"));
            var dataRe = new StringContent(jsonRe, Encoding.UTF8, "application/json");
            HttpResponseMessage responseRe = await _clientPaymentLogging.PostAsync("api/PaymentLogging", dataRe);
            if (!responseRe.IsSuccessStatusCode)
            {
                errorCount++;
                return Ok($"The Paymentlogging isn't created");
            }

            successCount++;
            
            //Send Mail to the company about the delivered order
            _messageGateway.SendMailMessage("BenjoCh@proton.me", employee.firstname, employee.lastname,
                receipt.Amount);
            
        }
        catch (Exception ex)
        {
            // Increment the error count and log the exception
            errorCount++;
            _logger.LogError(ex, "An error occurred while processing the order");
        }
        finally
        {
            // Calculate the duration of the process
            var duration = DateTime.Now - startTime;

            // Log the success and error rates
            _logger.LogInformation("Order processing complete. Success rate: {successRate}%, Error rate: {errorRate}%",
                successCount / (successCount + errorCount) * 100, errorCount / (successCount + errorCount) * 100);

            // Log the duration of the process
            _logger.LogInformation("Process duration: {duration}", duration);
        }

        return Ok("The order has been closed");
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
        List<OrderItemModel> items = JsonConvert.DeserializeObject<List<OrderItemModel>>(content) ??
                                     throw new Exception("There is something wrong with the receiving model");
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
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content) ??
                              throw new Exception("There is something wrong with the receiving model");
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
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content) ??
                              throw new Exception("There is something wrong with the receiving model");
        return Ok(item);
    }


    [HttpPut("orderItem")]
    public async Task<IActionResult> UpdateOrderItem(OrderItemModel model)
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
        OrderItemModel item = JsonConvert.DeserializeObject<OrderItemModel>(content) ??
                              throw new Exception("There is something wrong with the receiving model");


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