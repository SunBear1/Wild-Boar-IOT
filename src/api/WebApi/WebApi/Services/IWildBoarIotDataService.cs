using WebApi.Model;

namespace WebApi.Services
{
    public interface IWildBoarIotDataService
    {
        public IEnumerable<WildBoarIotData> GetWildBoarIotDataList();
        public WildBoarIotData GetWildBoarIotDataById(int id);
        public WildBoarIotData AddWildBoarIotData(WildBoarIotData wildBoarIotData);
        public WildBoarIotData UpdateWildBoarIotData(WildBoarIotData wildBoarIotData);
        public bool DeleteWildBoarIotData(int id);
    }
}