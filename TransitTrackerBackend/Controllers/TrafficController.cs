using Microsoft.AspNetCore.Mvc;
using TransitTracker.TrafficAPI.Services;
using TransitTracker.TrafficAPI.Models;

namespace TransitTracker.TrafficAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrafficController : ControllerBase
    {
        private readonly ITrafficService _trafficService;

        public TrafficController(ITrafficService trafficService)
        {
            _trafficService = trafficService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _trafficService.GetTrafficAsync();

            var logText = $"[{DateTime.Now}] API called – Returned {data.Count()} entries\n";
            Directory.CreateDirectory("/app/logs");
            await System.IO.File.AppendAllTextAsync("/app/logs/traffic.log", logText);

            return Ok(data);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string line)
        {
            var all = await _trafficService.GetTrafficAsync();
            var result = all.Where(x => x.Line.Contains(line, StringComparison.OrdinalIgnoreCase)).ToList();

            var logText = $"[{DateTime.Now}] Search called for '{line}' – Found {result.Count} entries\n";
            Directory.CreateDirectory("/app/logs");
            await System.IO.File.AppendAllTextAsync("/app/logs/traffic.log", logText);

            return Ok(result);
        }
    }
}

