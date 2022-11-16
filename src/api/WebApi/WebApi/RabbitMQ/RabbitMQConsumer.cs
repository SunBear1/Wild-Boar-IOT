using System.Collections.Immutable;
using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.Database;
using WebApi.Model;

namespace WebApi.RabbitMQ;

public class RabbitMQConsumer : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection connection;
    private IModel channel;
    private readonly IMongoCollection<WildBoarIotData> _dbContext;

    public RabbitMQConsumer(ILoggerFactory loggerFactory, IOptions<WildBoarIotDatabaseSettings> dbContext)
    {
        this._logger = loggerFactory.CreateLogger<RabbitMQConsumer>();
        var mongoClient = new MongoClient(
            dbContext.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbContext.Value.DatabaseName);
            
        _dbContext = mongoDatabase.GetCollection<WildBoarIotData>(
            dbContext.Value.CollectionName);
        InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            UserName="admin",
            Password = "1234",
            VirtualHost = "/vhost1",
            HostName = "message_broker",
            Port = 5672,
        };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        channel.BasicQos(prefetchSize:0, prefetchCount: 1, global: false);

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
            type = messageData.type,
            occupied = messageData.occupied,
            weight = messageData.weight,
            date = messageData.date
        };
        return messageWBIData;
    }  

    private void StoreMessage(WildBoarIotData wbiData)  
    {
        _logger.LogInformation("STORING MESSAGE");
        _dbContext.InsertOne(wbiData);
        // TODO Insert this object into mongoDB
    }  
    public override void Dispose()  
    {  
        channel.Close();  
        connection.Close();  
        base.Dispose();  
    }  
}  