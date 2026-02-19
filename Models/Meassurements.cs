using System.Text.Json.Serialization;

namespace DwdAzureSqlDataTransmitter.Models
{
    internal class Meassurements
    {
        [JsonPropertyName("dayDate")]
        public string Date { get; set; }

        public int TemperatureMin { get; set; }
        public int TemperatureMax { get; set; }
        public int Precipitation { get; set; }
        public int WindSpeed { get; set; }
        public int WindGust { get; set; }
        public int WindDirection { get; set; }
        public int Sunshine { get; set; }

        [JsonPropertyName("sunrise")]
        public long SunriseUnix { get; set; }

        [JsonPropertyName("sunset")]
        public long SunsetUnix { get; set; }

        [JsonPropertyName("moonPhase")]
        public int MoonPhase { get; set; }
    }
}
