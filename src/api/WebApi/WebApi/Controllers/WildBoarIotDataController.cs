using ChoETL;
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
        public async Task<List<WildBoarIotData>> Get(string? sort=null, string? type=null, int? weight=null,
            bool? occupied=null, DateTime? date_start=null, DateTime? date_end=null )
        {
            var getData = await wildBoarIotDataService.GetAsync();
            
            if (!sort.IsNull())
            {
                getData = getData.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x, null)).ToList();
            }

            if (!type.IsNull())
            {
                getData = getData.FindAll(x => x.type == type);
            }
            if (!weight.IsNull()) {
                getData = getData.FindAll(x => x.weight == weight);
            } 
            if (!occupied.IsNull()) {
                getData = getData.FindAll(x => x.occupied == occupied);
            } 
            if (!date_start.IsNull()) {
                getData = getData.FindAll(x => x.date >= date_start);
            } 
            if (!date_end.IsNull()) {
                getData = getData.FindAll(x => x.date <= date_end);
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