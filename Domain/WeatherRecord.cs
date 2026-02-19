namespace DwdAzureSqlDataTransmitter.Domain
{
    public class WeatherRecord
    {
        public int StationId { get; set; }
        public DateOnly Date { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public double Precipation { get; set; }
        public int WindSpeed { get; set; }
        public int WindGust { get; set; }
        public int WindDirection { get; set; }
        public TimeOnly? Sunshine { get; set; }
        public TimeOnly? Sunrise { get; set; }
        public TimeOnly? Sunset { get; set; }
        public int MoonPhase { get; set; }
    }
}
