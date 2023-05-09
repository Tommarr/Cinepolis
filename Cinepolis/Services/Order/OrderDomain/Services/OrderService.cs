using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderDomain.Models;
using OrderDomain.Repositories;
using RabbitMQ.Client;
using System.Text;

namespace OrderDomain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        //private readonly IRepository<Room> _repository;
        private readonly ILogger<OrderService> _logger;
        //private readonly THub _roomHub;
        public OrderService(IOrderRepository roomRepository, ILogger<OrderService> logger)
        {
            _logger = logger;
            _repository = roomRepository;
        }

        public Order CreateOrder(Order order)
        {
            Order createdOrder = _repository.Add(order);
            PublishOrder(createdOrder);
            return createdOrder;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _repository.GetAll();
            return orders;
        }

        public Order PublishOrder(Order order)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://guest:guest@host.docker.internal:5672");
            factory.ClientProvidedName = "OrderService";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "Cinepolis";
            string routingKey = "order-routing-key";
            string queueName = "OrderQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, true, false, false, null);
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
