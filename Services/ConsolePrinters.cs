using ApiJsonSqlServer.Models;
using System.Reflection;

namespace ApiJsonSqlServer.Services
{
    internal static class Extensions
    {
        public static void PrintProperties(this object obj)
        {
            if (obj == null) return;

            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var value = prop.GetValue(obj);
                Console.Write($"{prop.Name}:{value};");
            }
            Console.WriteLine();
        }
        public static void PrintDictionary(this Dictionary<string, StationData> dict)
        {
            if (dict == null || dict.Count == 0) return;

            foreach (var forecasts in dict)
            {
                Console.WriteLine($"StationId: {forecasts.Key}");

                var stationData = forecasts.Value;
                if (stationData.Meassurements != null)
                {
                    foreach (var day in stationData.Meassurements)
                    {
                        var props = day.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        
                        foreach (var prop in props)
                        {
                            var value = prop.GetValue(day);
                            Console.Write($"{prop.Name}:{value};");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
