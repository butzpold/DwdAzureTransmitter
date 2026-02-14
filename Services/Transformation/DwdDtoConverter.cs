using ApiJsonSqlServer.Domain;
using ApiJsonSqlServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiJsonSqlServer.Services
{
    internal class DwdDtoConverter
    {
    public static WeatherRecord Map(int stationId, Meassurements day)
        {
            return new WeatherRecord
            {
                StationId = stationId,
                Date = DateOnly.Parse(day.Date),
                TemperatureMin = day.TemperatureMin,
                TemperatureMax = day.TemperatureMax,
                Precipation = day.Precipitation,
                WindSpeed = day.WindSpeed,
                WindGust = day.WindGust,
                WindDirection = day.WindDirection,
                Sunshine = day.Sunshine,
                Sunrise = DateTimeOffset.FromUnixTimeSeconds(day.SunriseUnix/1000).UtcDateTime.ToString("HH:mm:ss"),
                Sunset = DateTimeOffset.FromUnixTimeSeconds(day.SunsetUnix/1000).UtcDateTime.ToString("HH:mm:ss"),
                MoonPhase = day.MoonPhase,
            };
        }
    }
}

