using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderDomain.Worker
{
    public class PaymentConsumer : BackgroundService
    {
        private readonly ILogger<PaymentConsumer> _logger;

        private string exchangeName;
        private string routingKey;
        private string queueName;
        private IModel channel;

        public PaymentConsumer(ILogger<PaymentConsumer> logger)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
            factory.ClientProvidedName = "OrderService";

            IConnection cnn = factory.CreateConnection();
            channel = cnn.CreateModel();

            exchangeName = "Cinepolis";
            routingKey = "payment-routing-key";
            queueName = "PaymentQueue";
            _logger = logger;
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
            try
            {
                //ConnectionFactory factory = new();
                //factory.Uri = new Uri("amqp://xgjivpda:oV4Cr6xUh1ORfcklh1mcoKoRWgA8WBaJ@rattlesnake.rmq.cloudamqp.com/xgjivpda");
                //factory.ClientProvidedName = "OrderService";

                //IConnection cnn = factory.CreateConnection();

                //IModel channel = cnn.CreateModel();

                //string exchangeName = "Cinepolis";
                //string routingKey = "payment-routing-key";
                //string queueName = "PaymentQueue";

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

                    channel.BasicAck(args.DeliveryTag, false);
                };

                string consumerTag = channel.BasicConsume(queueName, false, consumer);
            } catch(Exception ex)
            {
                _logger.LogInformation(ex.ToString());
            }
            

            //channel.BasicCancel(consumerTag);

            //channel.Close();
            //cnn.Close();
        }
    }
}
