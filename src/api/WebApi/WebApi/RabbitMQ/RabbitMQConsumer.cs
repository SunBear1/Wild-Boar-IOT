using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.Model;

namespace WebApi.RabbitMQ;

public class RabbitMQConsumer : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection connection;
    private IModel channel;

    public RabbitMQConsumer(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<RabbitMQConsumer>();
        InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            UserName="admin",
            Password = "1234",
            VirtualHost = "/vhost1",
            HostName = "localhost",
            Port = 17998
        };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();

    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"consumer received {message}");
            // Console.WriteLine(message);
            //var data = JsonConvert.DeserializeObject<WildBoarIotData>(message);
            var DataPayload = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(message);
            Console.WriteLine(DataPayload.ToString());
           // _logger.LogInformation(DataPayload.ToString());
            //Console.WriteLine(data);
        };
        channel.BasicConsume(queue: "WildBoarQueue",
            autoAck: true,
            consumer: consumer);
        return Task.CompletedTask;  
    }  
  
    private void HandleMessage(string content)  
    {  
        // we just print this message   
        Console.WriteLine(content);
        _logger.LogInformation($"consumer received {content}");
    }  

    public override void Dispose()  
    {  
        channel.Close();  
        connection.Close();  
        base.Dispose();  
    }  
}  
