using WebApi.Data;
using WebApi.Model;

namespace WebApi.Services
{
    public class WildBoarIotDataService: IWildBoarIotDataService
    {
        private readonly DbContextClass _dbContext;

        public WildBoarIotDataService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<WildBoarIotData> GetWildBoarIotDataList()
        {
            return _dbContext.Dataset.ToList();
        }

        public WildBoarIotData GetWildBoarIotDataById(int id)
        {
            // TYMCZASOWO BO NIE MAM ID :)
            return _dbContext.Dataset.Where(x => x.weights == id).FirstOrDefault();
        }

        public WildBoarIotData AddWildBoarIotData(WildBoarIotData wildBoarIotData)
        {
            var result = _dbContext.Dataset.Add(wildBoarIotData);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public WildBoarIotData UpdateWildBoarIotData(WildBoarIotData wildBoarIotData)
        {
            var result = _dbContext.Dataset.Update(wildBoarIotData);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteWildBoarIotData(int id)
        {
            var filteredData = _dbContext.Dataset.Where(x => x.weights == id).FirstOrDefault();
            var result = _dbContext.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }
    }
}