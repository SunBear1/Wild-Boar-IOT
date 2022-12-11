using ChoETL;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class data : ControllerBase
{
    private readonly IWildBoarIotDataService wildBoarIotDataService;

    public data(IWildBoarIotDataService _wildBoarIotDataService)
    {
        wildBoarIotDataService = _wildBoarIotDataService;
    }

    [HttpGet]
    public async Task<List<WildBoarIotData>> Get(string? sort = null, string? type = null, int? weight = null,
        bool? occupied = null, DateTime? date_start = null, DateTime? date_end = null, string? order = null)
    {
        var getData = await wildBoarIotDataService.GetAsync();

        if (order != "desc")
        {
            if (!sort.IsNull())
                getData = getData.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x, null)).ToList();
            else
                getData = getData.OrderBy(x => x.id).ToList();
        }
        else
        {
            if (!sort.IsNull())
                getData = getData.OrderByDescending(x => x.GetType().GetProperty(sort).GetValue(x, null)).ToList();
            else
                getData = getData.OrderByDescending(x => x.id).ToList();
        }

        if (!type.IsNull()) getData = getData.FindAll(x => x.type == type);
        if (!weight.IsNull()) getData = getData.FindAll(x => x.weight == weight);
        if (!occupied.IsNull()) getData = getData.FindAll(x => x.occupied == occupied);
        if (!date_start.IsNull()) getData = getData.FindAll(x => x.date >= date_start);
        if (!date_end.IsNull()) getData = getData.FindAll(x => x.date <= date_end);
        return getData;
    }

    [HttpGet("/api/dashboard")]
    public async Task<ActionResult<dashboardData>> Get()
    {
        var wildBoarData = await wildBoarIotDataService.GetAsync();

        var dData = new dashboardData();

        try
        {
            dData.lastReceivedMessage = wildBoarData.OrderByDescending(x => x.id).First();
        }
        catch (InvalidOperationException)
        {
            return dData;
        }

        var chest = wildBoarData.FindAll(x => x.type == "CHEST_MACHINE").TakeLast(100).ToList();
        var biceps = wildBoarData.FindAll(x => x.type == "BICEPS_MACHINE").TakeLast(100).ToList();
        var treadmill = wildBoarData.FindAll(x => x.type == "TREADMILL").TakeLast(100).ToList();

        if (chest.Count > 0)
        {
            dData.chestAVGweight = chest.Select(x => x.weight).Average();
            dData.chestAVGoccupancy = (int) (chest.Select(x => x.occupied ? 1.0 : 0.0).Average() * 100);
        }

        if (biceps.Count > 0)
        {
            dData.bicepsAVGweight = biceps.Select(x => x.weight).Average();
            dData.bicepsAVGoccupancy = (int) (biceps.Select(x => x.occupied ? 1.0 : 0.0).Average() * 100);
        }

        if (treadmill.Count > 0)
        {
            dData.treadmillAVGweight = treadmill.Select(x => x.weight).Average();
            dData.treadmillAVGoccupancy = (int) (treadmill.Select(x => x.occupied ? 1.0 : 0.0).Average() * 100);
        }

        return dData;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WildBoarIotData>> Get(long id)
    {
        var wildBoarData = await wildBoarIotDataService.GetAsync(id);

        if (wildBoarData is null) return NotFound();

        return wildBoarData;
    }

    [HttpPost]
    public async Task<IActionResult> Post(WildBoarIotData wildBoarIotData)
    {
        await wildBoarIotDataService.CreateAsync(wildBoarIotData);

        return CreatedAtAction(nameof(Get), new {wildBoarIotData.id}, wildBoarIotData);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, WildBoarIotData wildBoarIotData)
    {
        var data = await wildBoarIotDataService.GetAsync(id);

        if (data is null) return NotFound();

        wildBoarIotData.id = data.id;
        await wildBoarIotDataService.UpdateAsync(id, wildBoarIotData);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var data = await wildBoarIotDataService.GetAsync(id);

        if (data is null) return NotFound();

        await wildBoarIotDataService.RemoveAsync(id);

        return NoContent();
    }
}