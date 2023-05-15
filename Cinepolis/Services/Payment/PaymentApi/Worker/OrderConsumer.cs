using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderApi.Services;
using PaymentApi.Context;
using PaymentDomain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace PaymentApi.Worker
{
    public class OrderConsumer : BackgroundService
    {
        private readonly ILogger<OrderConsumer> _logger;
        //private readonly IOrderService _service;

        public readonly IServiceScopeFactory _serviceScopeFactory;

        private string exchangeName;
        private string routingKey;
        private string queueName;
        private IModel channel;

        public OrderConsumer(ILogger<OrderConsumer> logger, IServiceScopeFactory serviceScopeFactory)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
            factory.ClientProvidedName = "OrderService";

            IConnection cnn = factory.CreateConnection();
            channel = cnn.CreateModel();
            exchangeName = "Cinepolis";
            routingKey = "order-routing-key";
            queueName = "OrderQueue";

            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // When the timer should have no due-time, then do the work once now.

            DoWork();

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    DoWork();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private void DoWork()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                IOrderService orderService = scope.ServiceProvider.GetService<IOrderService>();

                //ConnectionFactory factory = new();
                //factory.Uri = new Uri("amqps://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
                //factory.ClientProvidedName = "PaymentService";

                //IConnection cnn = factory.CreateConnection();

                //IModel channel = cnn.CreateModel();

                //string exchangeName = "Cinepolis";
                //string routingKey = "order-routing-key";
                //string queueName = "OrderQueue";

                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(queueName, exchangeName, routingKey, null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, args) =>
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).Wait();
                    var body = args.Body.ToArray();

                    string message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($"Message Received: {message}");
                    Order order = JsonConvert.DeserializeObject<Order>(message);

                    orderService.CreateOrder(order);

                    channel.BasicAck(args.DeliveryTag, false);
                };

                string consumerTag = channel.BasicConsume(queueName, false, consumer);
            }


            

            //channel.BasicCancel(consumerTag);

            //channel.Close();
            //cnn.Close();
        }
    }
}
