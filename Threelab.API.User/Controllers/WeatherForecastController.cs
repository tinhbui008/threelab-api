using Microsoft.AspNetCore.Mvc;
using Threelab.Domain.Abstracts;
using Threelab.Domain.Entities;
using Threelab.Domain.Interfaces.Services;
using Threelab.Domain.Models.Error;
using Threelab.Service.Services;

namespace Threelab.API.User.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IApiKey _apiKeyService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApiKey apiKeyService)
        {
            _logger = logger;
            _apiKeyService = apiKeyService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public ResultObject Get()
        {
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return new SuccessResult<object>("SUCCESS", (int)HttpStatusCodes.OK, true, new { Data = "hihi" });
            //return Ok(new FailedResult("ERRORRRR", (int)HttpStatusCodes.CONFLICT));
        }

        [HttpPost(Name = "CreateApiKey")]
        public async Task<ResultObject> Post(ApiKey apiKey)
        {
            await _apiKeyService.GenerateKey();
            return new SuccessResult<object>("SUCCESS", (int)HttpStatusCodes.OK, true, new { Data = "hihi" });
            //return Ok(new FailedResult("ERRORRRR", (int)HttpStatusCodes.CONFLICT));
        }
    }
}
