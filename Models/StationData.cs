using System.Text.Json.Serialization;

namespace ApiJsonSqlServer.Models
{
    internal class StationData
    {
        [JsonPropertyName("days")]
        public List<Meassurements>? Meassurements { get; set; }
    }
}