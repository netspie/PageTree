using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PageTree.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //[HttpGet]
        //[Authorize]
        //public Task<ActionResult<WeatherForecast[]>> GetAuthorized()
        //{
        //    return Task.FromResult<ActionResult<WeatherForecast[]>>(
        //        Ok(Enumerable.Range(1, 50).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    }).ToArray()));
        //}

        [HttpGet]
        public Task<ActionResult<WeatherForecast[]>> Get()
        {
            return Task.FromResult<ActionResult<WeatherForecast[]>>(
                Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray()));
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public class WeatherForecast
        {
            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
            public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
        }
    }
}
