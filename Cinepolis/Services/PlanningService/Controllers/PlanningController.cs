using Microsoft.AspNetCore.Mvc;
using PlanningService.Models;

namespace PlanningService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanningController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<PlanningController> _logger;

        public PlanningController(ILogger<PlanningController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "PostPlanning")]
        public Planning Post(Planning planning)
        {
            PlanningService.Services.PlanningService a  = new();
            a.ProcesPlanning(planning);
            return planning;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
