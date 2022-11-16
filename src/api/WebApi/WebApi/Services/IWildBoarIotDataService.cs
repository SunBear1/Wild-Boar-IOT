using WebApi.Model;

namespace WebApi.Services
{
    public interface IWildBoarIotDataService
    {
        public Task<List<WildBoarIotData>> GetAsync();
        public Task<WildBoarIotData?> GetAsync(long id);
        public Task CreateAsync(WildBoarIotData wildBoarIotData);
        public Task UpdateAsync(long id,WildBoarIotData wildBoarIotData);
        public Task RemoveAsync(long id);
    }
}