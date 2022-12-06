using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WildBoarIotDataController : ControllerBase
    {
        private readonly IWildBoarIotDataService wildBoarIotDataService;

        public WildBoarIotDataController(IWildBoarIotDataService _wildBoarIotDataService)
        {
            wildBoarIotDataService = _wildBoarIotDataService;
        }

        [HttpGet]
        [Route("{sort?}/{format?}/{type?}/{weight?}/{occupied?}/{date_start?}/{date_end?}")]
        public async Task<List<WildBoarIotData>> Get( DateTime? date_start, DateTime? date_end, string sort=" ", string format=" ", string type=" ", int weight=0,
            bool occupied=false)
        {
            var getData = await wildBoarIotDataService.GetAsync();

            if (sort != " ")
            {
                Console.Write(getData);
                Console.Write("here");
                getData = getData.OrderBy(x => "x." + sort).ToList();
            }
            if (type != " ")
            {
                getData = getData.FindAll(x => x.type == type);
            } 
            if (weight != null) {
                getData = getData.FindAll(x => x.weight == weight);
            } 
            if (occupied != null) {
                getData = getData.FindAll(x => x.occupied == occupied);
            } 
            if (date_start != null) {
                getData = getData.FindAll(x => x.date >= date_start);
            } 
            if (date_end != null) {
                getData = getData.FindAll(x => x.date <= date_end);
            }
            if (format == "csv")
            {
                // I have no clue. Docker police pls help!
            }
            
            return getData;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WildBoarIotData>> Get(long id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, WildBoarIotData wildBoarIotData)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
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