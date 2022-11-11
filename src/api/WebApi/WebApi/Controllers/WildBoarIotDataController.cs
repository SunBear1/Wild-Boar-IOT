using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.RabbitMQ;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WildBoarIotDataController : ControllerBase
    {
        private readonly IWildBoarIotDataService wildBoarIotDataService;
        private readonly IRabbitMQProducer rabbitMqProducer;

        public WildBoarIotDataController(IWildBoarIotDataService _wildBoarIotDataService,
            IRabbitMQProducer _rabbitMqProducer)
        {
            wildBoarIotDataService = _wildBoarIotDataService;
            rabbitMqProducer = _rabbitMqProducer;
        }

        [HttpGet("wildBoarIotDataList")]
        public IEnumerable<WildBoarIotData> WildBoarIotDataList()
        {
            var wildBoarIotDataList = wildBoarIotDataService.GetWildBoarIotDataList();
            return wildBoarIotDataList;
        }

        [HttpGet("wildBoarIotDataById")]
        public WildBoarIotData GetWildBoarIotDataById(int id)
        {
            return wildBoarIotDataService.GetWildBoarIotDataById(id);
        }

        [HttpPost("addWildBoarIotData")]
        public WildBoarIotData AddWildBoarIotData(WildBoarIotData wildBoarIotData)
        {
            var wildBoarIotDataTemp = wildBoarIotDataService.AddWildBoarIotData(wildBoarIotData);
            rabbitMqProducer.SendWildBoarIotDataMessage(wildBoarIotDataTemp);
            return wildBoarIotDataTemp;
        }

        [HttpPut("updateWildBoarIotData")]
        public WildBoarIotData UpdateWildBoarIotData(WildBoarIotData wildBoarIotData)
        {
            return wildBoarIotDataService.UpdateWildBoarIotData(wildBoarIotData);
        }

        [HttpDelete("deleteWildBoarIotData")]
        public bool DeleteWildBoarIotData(int id)
        {
            return wildBoarIotDataService.DeleteWildBoarIotData(id);
        }
    }
}