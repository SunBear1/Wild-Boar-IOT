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

        [HttpGet()]
        public async Task<List<WildBoarIotData>> Get() =>
            await wildBoarIotDataService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<WildBoarIotData>> Get(string id)
        {
            var wildBoarData = await wildBoarIotDataService.GetAsync(id);

            if (wildBoarData is null)
            {
                return NotFound();
            }

            return wildBoarData;
        }

        [HttpPost]
        public async Task<IActionResult> Post(WildBoarIotData wildBoarIotData)
        {
            await wildBoarIotDataService.CreateAsync(wildBoarIotData);

            return CreatedAtAction(nameof(Get), new { id = wildBoarIotData.id }, wildBoarIotData);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, WildBoarIotData wildBoarIotData)
        {
            var data = await wildBoarIotDataService.GetAsync(id);

            if (data is null)
            {
                return NotFound();
            }

            wildBoarIotData.id = data.id;
            await wildBoarIotDataService.UpdateAsync(id, wildBoarIotData);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await wildBoarIotDataService.GetAsync(id);

            if (data is null)
            {
                return NotFound();
            }

            await wildBoarIotDataService.RemoveAsync(id);

            return NoContent();
        }
    }
}