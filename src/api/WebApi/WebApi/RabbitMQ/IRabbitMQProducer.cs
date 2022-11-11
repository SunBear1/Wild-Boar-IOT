namespace WebApi.RabbitMQ
{
    public interface IRabbitMQProducer
    {
        public void SendWildBoarIotDataMessage < T > (T message);
    }
}