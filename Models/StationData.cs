using System.Text.Json.Serialization;

namespace DwdAzureSqlDataTransmitter.Models
{
    internal class StationData
    {
        [JsonPropertyName("days")]
        public List<Meassurements>? Meassurements { get; set; }
    }
}