using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace WebApi.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public RabbitMQProducer()
        {
            Console.WriteLine("DUPSKO");
            var factory = new ConnectionFactory
            {
                UserName="admin",
                Password = "1234",
                VirtualHost = "/vhost1",
                HostName = "message_broker",
                Port = 17998
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare("WildBoarQueue", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[X] {0}", message);
            };
            channel.BasicConsume(queue:"WildBoarQueue",
                autoAck: true,
                consumer: consumer);
            
            Console.WriteLine("SDADADASDASDD");
            // var json = JsonConvert.SerializeObject(message);
            // var body = Encoding.UTF8.GetBytes(json);
            // channel.BasicPublish(exchange: "", routingKey: "id", body: body);
        }
    }
}