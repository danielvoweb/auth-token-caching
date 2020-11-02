using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Basic.Entities;

namespace Client.Basic
{
    public class WeatherForecastApiClient : IApiClient<WeatherForecast>
    {
        private readonly HttpClient _httpClient;
        public WeatherForecastApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiResourceClient");
        }

        public Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}