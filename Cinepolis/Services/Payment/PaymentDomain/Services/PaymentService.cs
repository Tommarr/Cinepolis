using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentDomain.Models;
using PaymentDomain.Repositories;
using RabbitMQ.Client;
using System.Text;

namespace OrderApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        private readonly ILogger<PaymentService> _logger;

        public PaymentService(ILogger<PaymentService> logger, IPaymentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public Payment CreatePayment(Payment payment)
        {
            Payment createdPayment = _repository.Add(payment);
            return createdPayment;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            IEnumerable<Payment> payments = _repository.GetAll();
            return payments;
        }

        public Payment PublishPayment(Payment payment)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://guest:guest@host.docker.internal:5672");
            factory.ClientProvidedName = "PaymentService";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "Cinepolis";
            string routingKey = "payment-routing-key";
            string queueName = "PaymentQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
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
