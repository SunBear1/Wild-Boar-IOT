using System.Text;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApi.Database;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.RabbitMQ;

public class RabbitMQConsumer : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection connection;
    private IModel channel;
    private readonly IMongoCollection<WildBoarIotData> _dbContext;
    private int store_data_counter = 0;
    private int get_mesg_counter = 0;

    public RabbitMQConsumer(ILoggerFactory loggerFactory, IOptions<WildBoarIotDatabaseSettings> dbContext)
    {
        _logger = loggerFactory.CreateLogger<RabbitMQConsumer>();
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
            HostName = "localhost",
            Port = 17998
        };

        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        _logger.LogInformation($"BEGIN_GET_MSG");
        var consumer = new EventingBasicConsumer(channel);
        //channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"API received following message: {message}");
            var wbiData = ConvertMessageToWBIData(message);
            StoreMessage(wbiData);
            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };
        channel.BasicConsume(queue: "WildBoarQueue",
            autoAck: false,
            consumer: consumer);
        return Task.CompletedTask;
    }  
  
    private WildBoarIotData ConvertMessageToWBIData(string message)
    {
        var messageWBIData = JsonConvert.DeserializeObject<WildBoarIotData>(message);
        return messageWBIData;
        //return new WildBoarIotData();
    }  

    
    
    private void StoreMessage(WildBoarIotData wbiData)
    {
        store_data_counter += 1;
        _logger.LogInformation("Storing data...");
        //await _dbContext.InsertOneAsync(wbiData);
    }  
    public override void Dispose()  
    {  
        channel.Close();  
        connection.Close();  
        base.Dispose();  
    }  
}  
