using Microsoft.AspNetCore.Connections;
using PaymentService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;

        }

        public Payment ProcesPayment(Payment payment)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://guest:guest@host.docker.internal:5672");
            factory.ClientProvidedName = "PaymentService";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "PaymentExchange";
            string routingKey = "payment-routing-key";
            string queueName = "PaymentQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var json = JsonConvert.SerializeObject(payment);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchangeName, routingKey, null, body);
            _logger.LogInformation($"Message published to {queueName}");

            channel.Close();
            cnn.Close();

            return payment;
        }
    }
}
