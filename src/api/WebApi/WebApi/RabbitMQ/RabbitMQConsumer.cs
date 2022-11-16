using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            _logger.LogInformation($"API received following message: {message}");
            var wbiData = ConvertMessageToWBIData(message);
            StoreMessage(wbiData);
            ConvertMessageToWBIData(message);
        };
        channel.BasicConsume(queue: "WildBoarQueue",
            autoAck: true,
            consumer: consumer);
        return Task.CompletedTask;  
    }  
  
    private WildBoarIotData ConvertMessageToWBIData(string message)  
    {
        var messageData = JsonConvert.DeserializeObject<dynamic>(message);
        var messageWBIData = new WildBoarIotData
        {
            id = messageData.id,
            occupied = messageData.occupied,
            weight = messageData.weight,
            date = messageData.date
        };
        return messageWBIData;
    }  

    private void StoreMessage(WildBoarIotData wbiData)  
    {
        // TODO Insert this object into mongoDB
    }  
    public override void Dispose()  
    {  
        channel.Close();  
        connection.Close();  
        base.Dispose();  
    }  
}  
