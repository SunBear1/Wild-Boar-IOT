using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApi.RabbitMQ;

public class RabbitMQConsumer : BackgroundService
{
    private readonly ILogger _logger;
    private IConnection _connection;
    private IModel _channel;

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
        Console.WriteLine("dupa");
        _connection = factory.CreateConnection();
        // create channel  
        _channel = _connection.CreateModel();  
        Console.WriteLine("dupa");

        _channel.ExchangeDeclare("WildBoarQueue", ExchangeType.Topic);  
        _channel.QueueDeclare("WildBoarQueue.queue.log", false, false, false, null);  
        _channel.QueueBind("WildBoarQueue.queue.log", "WildBoarQueue", "WildBoarQueue", null);  
        _channel.BasicQos(0, 1, false);  

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)  
    {  
        stoppingToken.ThrowIfCancellationRequested();  
        Console.WriteLine("dupa");
        Console.WriteLine("logger");
        var consumer = new EventingBasicConsumer(_channel);  
        consumer.Received += (ch, ea) =>  
        {  
            // received message 
            Console.WriteLine(ea.Body);
            Console.WriteLine("dupa");


            var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());  
            Console.WriteLine(content);

            // handle the received message  
            HandleMessage(content);  
            _channel.BasicAck(ea.DeliveryTag, false);  
        };  
  
        consumer.Shutdown += OnConsumerShutdown;  
        consumer.Registered += OnConsumerRegistered;  
        consumer.Unregistered += OnConsumerUnregistered;  
        consumer.ConsumerCancelled += OnConsumerConsumerCancelled;  
  
        _channel.BasicConsume("WildBoarQueue.queue.log", false, consumer);  
        return Task.CompletedTask;  
    }  
  
    private void HandleMessage(string content)  
    {  
        // we just print this message   
        Console.WriteLine(content);
        _logger.LogInformation($"consumer received {content}");
    }  
    private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e)  {  }  
    private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerRegistered(object sender, ConsumerEventArgs e) {  }  
    private void OnConsumerShutdown(object sender, ShutdownEventArgs e) {  }  
    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)  {  }  
  
    public override void Dispose()  
    {  
        _channel.Close();  
        _connection.Close();  
        base.Dispose();  
    }  
}  
