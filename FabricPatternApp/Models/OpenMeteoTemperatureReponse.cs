namespace FabricPatternApp.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Ответ API
    /// </summary>
    public class OpenMeteoTemperatureReponse
    {

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double Generationtime_ms { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int Utc_offset_seconds { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string Timezone_abbreviation { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("daily_units")]
        public DailyUnits DailyUnits { get; set; }

        [JsonPropertyName("daily")]
        public Daily Daily { get; set; }
    }


    public class Daily
    {
        [JsonPropertyName("time")]
        public List<DateTime> Time { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public List<double> TemperatureMax { get; set; }
    }

    public class DailyUnits
    {
        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public string TemperatureMax { get; set; }
    }
}
