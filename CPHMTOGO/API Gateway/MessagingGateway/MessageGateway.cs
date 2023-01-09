using System.Text;
using APIGateway.Models.OrderService;
using Experimental.System.Messaging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace APIGateway.MessagingGateway;

public class MessageGateway
{
    private readonly IConnectionFactory _connectionFactory;

    public MessageGateway(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void SendMailMessage(string email,string restaurantname, string firstname, string lastname,double amount ,List<CreateOrderItemModel> orderItems)
    {
        using (var connection = _connectionFactory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare queue
            channel.QueueDeclare(queue: "OrderProcess", durable: false, exclusive: false, autoDelete: false, arguments: null);   
            
            // Set the message properties
            var properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.Type = "json";
            //Create Message
            var message = new
            {
                email,
                restaurantname, 
                firstname,
                lastname, 
                amount,
                orderItems
            };
            
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            
            // Publish the message to the queue
            channel.BasicPublish(exchange: "", routingKey: "OrderProcess", basicProperties: properties, body: body);
        }
    }
    public void SendMailMessage(string email, string name,double amount ,List<CreateOrderItemModel> orderItems)
    {
        using (var connection = _connectionFactory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare queue
            channel.QueueDeclare(queue: "OrderProcess", durable: false, exclusive: false, autoDelete: false, arguments: null); 
            
            // Set the message properties
            var properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.Type = "json";
            
            //Create Message
            var message = new
            {
                email,
                name,
                amount,
                orderItems
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            
            // Publish the message to the queue
            channel.BasicPublish(exchange: "", routingKey: "OrderProcess", basicProperties: properties, body: body);
        }
    }

    public void SendMailMessage(string email, string firstname, string lastname, double amount)
    {
        using (var connection = _connectionFactory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            // Declare queue
            channel.QueueDeclare(queue: "OrderProcess", durable: false, exclusive: false, autoDelete: false, arguments: null); 
            
            // Set the message properties
            var properties = channel.CreateBasicProperties();
            properties.ContentType = "application/json";
            properties.Type = "json";
            
            //Create Message
            var message = new
            {
                Email = email,
                FirstName = firstname,
                LastName =lastname,
                Amount = amount,
            };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            
            // Publish the message to the queue
            channel.BasicPublish(exchange: "", routingKey: "OrderProcess", basicProperties: properties, body: body);
        }    }


}