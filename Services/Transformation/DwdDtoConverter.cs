using DwdAzureSqlDataTransmitter.Domain;
using DwdAzureSqlDataTransmitter.Models;

namespace DwdAzureSqlDataTransmitter.Services
{
    internal class DwdDtoConverter
    {
        private static TimeOnly? ConvertUnixMsToTime(long? unixMs)
        {
            // if API sends a negative/null-value timestamp
            if (unixMs == null || unixMs <= 0)
                return null;           
            try
            {
                var dateTime = DateTimeOffset
                    // intervenes together with try/catch
                    // if the timestamp isn't a valid number;
                    // mostly insane large (9999999999999999999)
                    .FromUnixTimeMilliseconds(unixMs.Value)
                    .ToLocalTime()
                    .DateTime;

                var time = dateTime.TimeOfDay;
                // if timespan is invalid; has to be between TimeSpan.Zero (=00:00:00) &
                // TimeSpan.FromDays(1) (=23:59:59.9999999)
                if (time < TimeSpan.Zero || time >= TimeSpan.FromDays(1))
                    return null;

                return TimeOnly.FromTimeSpan(time);
            }
            catch
            {
                // If API sends completely broken timestamp
                return null;
            }
        }
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

