using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace WebApi.RabbitMQ
{
    public class RabbitMQProducer: IRabbitMQProducer
    {
        public void SendWildBoarIotDataMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                UserName="admin",
                Password = "1234",
                VirtualHost = "/vhost1",
                HostName = "message_broker",
                Port = 5672
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();
            channel.QueueDeclare("weights", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "weights", body: body);
        }
    }
}