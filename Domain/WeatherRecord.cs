namespace ApiJsonSqlServer.Domain
{
    public class WeatherRecord
    {
        public int StationId { get; set; }
        public DateOnly Date { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public int Precipation { get; set; }
        public int WindSpeed { get; set; }
        public int WindGust { get; set; }
        public int WindDirection { get; set; }
        public int Sunshine { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public int MoonPhase { get; set; }
    }
}
