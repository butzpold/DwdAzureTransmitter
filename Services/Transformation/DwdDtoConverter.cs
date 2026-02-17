using ApiJsonSqlServer.Domain;
using ApiJsonSqlServer.Models;

namespace ApiJsonSqlServer.Services
{
    internal class DwdDtoConverter
    {
        private static TimeOnly ConvertUnixMsToTime(long unixMs) =>
            TimeOnly.FromDateTime(
                DateTimeOffset
                    .FromUnixTimeMilliseconds(unixMs)
                    .ToLocalTime()
                    .DateTime
                );
        private static double ScaleToDouble(int value) => value / 10.0;
        private static int ScaleToInt(int value) 
            => (int)Math.Round(ScaleToDouble(value), MidpointRounding.AwayFromZero);


        public static WeatherRecord Map(int stationId, Meassurements day)
        {
            return new WeatherRecord
            {
                StationId = stationId,
                Date = DateOnly.Parse(day.Date),
                TemperatureMin = ScaleToDouble(day.TemperatureMin),
                TemperatureMax = ScaleToDouble(day.TemperatureMax),
                Precipation = ScaleToDouble(day.Precipitation),
                WindSpeed = ScaleToInt(day.WindSpeed),
                WindGust = ScaleToInt(day.WindGust),
                WindDirection = ScaleToInt(day.WindDirection),
                Sunshine = TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(ScaleToInt(day.Sunshine))),
                Sunrise = ConvertUnixMsToTime(day.SunriseUnix),                                   
                Sunset = ConvertUnixMsToTime(day.SunsetUnix),
                MoonPhase = day.MoonPhase,
            };
        }
    }
}

