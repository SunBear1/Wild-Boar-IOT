using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApi.Database;
using WebApi.Model;

namespace WebApi.RabbitMQ;

public class RabbitMQService
{
    private readonly IMongoCollection<WildBoarIotData> _dbContext;
    private int dupa = 5;
    public RabbitMQService(IOptions<WildBoarIotDatabaseSettings> dbContext)
    {
        var mongoClient = new MongoClient(
            dbContext.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            dbContext.Value.DatabaseName);
            
        _dbContext = mongoDatabase.GetCollection<WildBoarIotData>(
            dbContext.Value.CollectionName);
    }
    
    public async Task InsertMessage(WildBoarIotData wildBoarIotData) =>
        await _dbContext.InsertOneAsync(wildBoarIotData);
    
}