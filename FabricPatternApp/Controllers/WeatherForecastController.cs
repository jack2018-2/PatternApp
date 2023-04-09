using FabricPatternApp.Factory;
using FabricPatternApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace PatternApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        public WeatherController(){ }

        [HttpGet]
        [Route("moscow")]
        public async Task<OpenMeteoTemperatureReponse?> GetMoscowWeatherForecastAsync() 
        {
           var api = new MoscowWeatherApi();
           return await api.ExecuteRequestAsync();
        }

        [HttpGet]
        [Route("saint_petersburg")]
        public async Task<OpenMeteoTemperatureReponse?> GetSpbWeatherForecastAsync()
        {
           var api = new SaintPetersburgWeatherApi();
           return await api.ExecuteRequestAsync();
        }
    }
}