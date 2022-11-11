using WebApi.Model;

namespace WebApi.Services
{
    public interface IWildBoarIotDataService
    {
        public Task<List<WildBoarIotData>> GetAsync();
        public Task<WildBoarIotData?> GetAsync(string id);
        public Task CreateAsync(WildBoarIotData wildBoarIotData);
        public Task UpdateAsync(string id,WildBoarIotData wildBoarIotData);
        public Task RemoveAsync(string id);
    }
}