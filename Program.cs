using ApiJsonSqlServer.Domain;
using ApiJsonSqlServer.Services;
using ApiJsonSqlServer.Services.Export;
using ApiJsonSqlServer.Services.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiJsonSqlServer
{
    internal class Program
    {        
        static async Task Main(string[] args)
        {
            // Sets the connection to the Azure DB
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AzureDbContext>(options =>
                        options.UseSqlServer(
                            // the Connetion String is defined in the appsettings.json
                            context.Configuration.GetConnectionString("AzureSql")));

                    services.AddScoped<AzureDbClient>();
                    services.AddScoped<AzureDbExportService>();
                    services.AddScoped<DwdApiClient>();
                })
                .Build();

            var dwdClient = host.Services.GetRequiredService<DwdApiClient>();
            var exportService = host.Services.GetRequiredService<AzureDbExportService>();         
            
            // DWD Download
            string filepath = "WeatherStations_API.csv";

            var stations = CsvReader.Read(filepath);

            var dailyRecords = new List<WeatherRecord>();

            var dwdApiClient = new DwdApiClient();

            int Count = 0;

            Console.WriteLine("ApiParameterFetcher");
            Console.WriteLine("------------------");

            foreach (var station in stations)
            {
                try
                {
                    // getting the according json-File from the API
                    string json = await dwdApiClient.GetStationDataAsync(station.StationKe);

                    // deserializing the json-File (in a dictionnary)
                    var jsonParsed = DwdApiParser.Parse(json);

                    if (jsonParsed.TryGetValue(station.StationKe, out var stationData))
                    {
                        // just taking the needed entries out of the dictionnary 
                        // and creating an object out of it
                        var latestData = stationData.Meassurements?.FirstOrDefault();
                        if (latestData != null)
                        {
                            // converting the datatypes of the object and appending the StationId
                            // to prepare the Data for the upload
                            var record = DwdDtoConverter.Map(station.StationId, latestData);
                            dailyRecords.Add(record);
                        }
                    }

                    Count++;                               

                    await Task.Delay(200); // optional rate limiting
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for {station.StationKe}: {ex.Message}");
                }
            }
            // starts uploading to DB after all dailyRecords collected            
            await exportService.ExportAsync(dailyRecords);

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
