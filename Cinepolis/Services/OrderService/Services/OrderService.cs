using Microsoft.AspNetCore.Connections;
using PaymentService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PaymentService.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;

        }

        public Order ProcesOrder(Order order)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://guest:guest@host.docker.internal:5672");
            factory.ClientProvidedName = "OrderService";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "OrderExchange";
            string routingKey = "order-routing-key";
            string queueName = "OrderQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var json = JsonConvert.SerializeObject(order);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchangeName, routingKey, null, body);
            _logger.LogInformation($"Message published to {queueName}");

            channel.Close();
            cnn.Close();

            return order;
        }
    }
}
