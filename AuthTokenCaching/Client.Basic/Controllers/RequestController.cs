using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Basic.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Client.Basic.Controllers
{
    public class RequestController : ControllerBase
    {
        private readonly IApiClient<WeatherForecast> _weatherForecastApiClient;

        public RequestController(IApiClient<WeatherForecast> weatherForecastApiClient)
        {
            _weatherForecastApiClient = weatherForecastApiClient;
        }

        [HttpGet("weather")]
        public async Task<IEnumerable<WeatherForecast>> GetWeather()
        {
            var weatherForecast = await _weatherForecastApiClient.GetAllAsync();
            return weatherForecast;
        }
    }
}