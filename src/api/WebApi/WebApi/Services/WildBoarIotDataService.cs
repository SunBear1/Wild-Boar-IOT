using Microsoft.Extensions.Options;
using WebApi.Model;
using MongoDB.Driver;
using WebApi.Database;

namespace WebApi.Services
{
    public class WildBoarIotDataService: IWildBoarIotDataService
    {
        private readonly IMongoCollection<WildBoarIotData> _dbContext;

        public WildBoarIotDataService(IOptions<WildBoarIotDatabaseSettings> dbContext)
        {
            var mongoClient = new MongoClient(
                dbContext.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                dbContext.Value.DatabaseName);
            
            _dbContext = mongoDatabase.GetCollection<WildBoarIotData>(
                dbContext.Value.CollectionName);
        }

        public async Task<List<WildBoarIotData>> GetAsync() =>
            await _dbContext.Find(_ => true).ToListAsync();

        public async Task<WildBoarIotData?> GetAsync(long id) =>
            await _dbContext.Find(x => x.id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(WildBoarIotData wildBoarIotData) =>
            await _dbContext.InsertOneAsync(wildBoarIotData);

        public async Task UpdateAsync(long id, WildBoarIotData wildBoarIotData) =>
            await _dbContext.ReplaceOneAsync(x => x.id == id, wildBoarIotData);

        public async Task RemoveAsync(long id) =>
            await _dbContext.DeleteOneAsync(x => x.id == id);
    }
}