using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderDomain.Models;
using OrderDomain.Repositories;
using RabbitMQ.Client;
using System.Data;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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

        public async Task<Order> CreateOrderAsync(Order order)
        {

            order.CreatedAt = await GetDateAsync();
            Order createdOrder = _repository.Add(order);
            PublishOrder(createdOrder);
            return createdOrder;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            
            var orders = await _repository.GetAll();
            return orders;
        }

        public async Task<DateTime> GetDateAsync()
        {
            DateTime createdAt = new DateTime();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cinecloud.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/HttpTrigger1?code=-u-qrVFiVX3uCdB4iidd5VU2ddPyIQ3332oh4N2eXcsSAzFu62Jq1w==");
                if (response.IsSuccessStatusCode)
                {
                    string date = await response.Content.ReadAsStringAsync();

                    date = date.Replace("\"", "");

                    _logger.LogInformation($"received: {date}");

                    createdAt = DateTime.Parse(date);
                }
                else
                {
                    _logger.LogInformation($"Internal server Error");
                }
            }
            return createdAt;
        }

        public Order PublishOrder(Order order)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
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
