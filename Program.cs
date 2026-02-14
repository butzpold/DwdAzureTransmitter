using ApiJsonSqlServer.Services;
using ApiJsonSqlServer.Services.Import;

namespace ApiJsonSqlServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string filepath = "WeatherStations_API.csv";

            var stations = CsvReader.Read(filepath);     
            
            var dwdApiClient = new DwdApiClient();

            int Count = 0;

            Console.WriteLine("ApiParameterFetcher");
            Console.WriteLine("------------------");

            foreach (var station in stations)
            {
                try
                {
                    // JSON von API holen
                    string json = await dwdApiClient.GetStationDataAsync(station.StationKe);

                    // JSON in RootResponse deserialisieren
                    var jsonParsed = DwdApiParser.Parse(json);

                    //Console.Write(json);   

                    jsonParsed.PrintDictionary();

                    if (jsonParsed.TryGetValue(station.StationKe, out var stationData))
                    {
                        var latestData = stationData.Meassurements?.FirstOrDefault();
                        if (latestData != null)
                        {
                            var record = DwdDtoConverter.Map(station.StationId, latestData);
                            latestData.PrintProperties();
                            record.PrintProperties();
                        }
                    }

                    Count++;                               

                    await Task.Delay(200); // optional rate limiting
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for {station.StationKe}: {ex.Message}");
                }
                break;
            }
            //Console.Clear();
            Console.WriteLine("ApiParameterFetcher");
            Console.WriteLine("------------------");

            Console.WriteLine(Count + " Stations were parsed");

            Console.WriteLine("------------------");
            Console.Write("Press any Key to quit");

            Console.ReadKey();

        }
    }
}
