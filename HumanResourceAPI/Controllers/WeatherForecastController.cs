using Entities.Models;
using HumanResourceAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repositoryManager;

        public WeatherForecastController(ILoggerManager logger, IRepositoryManager repositoryManager)
        {
            _logger = logger;
            _repositoryManager = repositoryManager;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Company> Get()
        {

            return _repositoryManager.Company.FindAll(trackChange: false).ToList();
            //_logger.LogDebug("This is debug from WeatherController");
            //_logger.LogError("This is error from WeatherController");
            //_logger.LogInfo("This is info from WeatherController");
            //_logger.LogWarn("This is warn from WeatherController");


            //return Enumerable.Range(1, 4).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}