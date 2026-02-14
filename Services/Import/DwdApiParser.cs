using ApiJsonSqlServer.Models;
using System.Text.Json;

namespace ApiJsonSqlServer.Services.Import
{
    internal class DwdApiParser
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static Dictionary<string, StationData> Parse(string json)
        {
            return JsonSerializer.Deserialize<Dictionary<string, StationData>>(json, _options);
        }
    }
}
