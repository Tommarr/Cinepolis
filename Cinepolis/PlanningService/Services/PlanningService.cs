using RabbitMQ.Client;
using PlanningService.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using Newtonsoft.Json;

namespace PlanningService.Services
{
    public class PlanningService
    {

        public PlanningService() {

        }

        public Planning ProcesPlanning(Planning planning)
        {
            ConnectionFactory factory = new();
            factory.Uri = new Uri("amqp://guest:guest@host.docker.internal:5672");
            factory.ClientProvidedName = "PlanningService";

            IConnection cnn = factory.CreateConnection();

            IModel channel = cnn.CreateModel();

            string exchangeName = "DemoExchange";
            string routingKey = "demo-routing-key";
            string queueName = "DemoQueue";

            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routingKey, null);

            var json = JsonConvert.SerializeObject(planning);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchangeName, routingKey, null, body);

            channel.Close();
            cnn.Close();

            return planning;
        }
    }
}
