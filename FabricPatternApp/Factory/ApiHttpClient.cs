using System.Text.Json;
using FabricPatternApp.Models;

namespace FabricPatternApp.Factory
{
    /// <summary>
    /// абстрактная фабрика веб-клиентов
    /// </summary>
    public abstract class ApiHttpClient : IApiHttpClientFactory
    {
        private HttpClient _httpClient;

        protected HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(@$"https://api.open-meteo.com/v1/forecast");
            }
            return _httpClient;
        }

        protected abstract Task<OpenMeteoTemperatureReponse> Get();

        /// <summary>
        /// Выполнить связанный с этим клиентом Get-запрос и вернуть результат
        /// </summary>
        /// <returns><see cref="Task"/> содержащий строковый ответ</returns>
        public async Task<OpenMeteoTemperatureReponse?> ExecuteRequestAsync()
        {
            return await Get();
        }
    }

    /// <summary>
    /// фабрика веб-клиентов api с погодой в мск
    /// </summary>
    public class MoscowWeatherApi : ApiHttpClient
    {
        private readonly HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,
            $"?timezone=Europe%2FMoscow&daily=temperature_2m_max&latitude=55.75&longitude=37.62&start_date={DateTime.Now.ToString("yyyy-MM-dd")}&end_date={DateTime.Now.ToString("yyyy-MM-dd")}");

        protected override async Task<OpenMeteoTemperatureReponse?> Get()
        {
            OpenMeteoTemperatureReponse? apiResponse;
            using (HttpClient client = GetHttpClient())
            {
                var responseRaw = await client.SendAsync(request);
                var json = await responseRaw.Content.ReadAsStringAsync();
                apiResponse = JsonSerializer.Deserialize<OpenMeteoTemperatureReponse>(json);
            }
            return apiResponse;
        }
    }

    /// <summary>
    /// фабрика веб-клиентов api с погодой в спб
    /// </summary>
    public class SaintPetersburgWeatherApi : ApiHttpClient
    {
        private readonly HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, 
            $"?timezone=Europe%2FMoscow&daily=temperature_2m_max&latitude=59.94&longitude=30.31&start_date={DateTime.Now.ToString("yyyy-MM-dd")}&end_date={DateTime.Now.ToString("yyyy-MM-dd")}"); 

        protected override async Task<OpenMeteoTemperatureReponse?> Get()
        {
            OpenMeteoTemperatureReponse? apiResponse;
            using (HttpClient client = GetHttpClient())
            {
                var responseRaw = await client.SendAsync(request);
                var json = await responseRaw.Content.ReadAsStringAsync();
                apiResponse = JsonSerializer.Deserialize<OpenMeteoTemperatureReponse>(json);
            }
            return apiResponse;
        }
    }
}
