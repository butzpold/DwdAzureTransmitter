using DwdAzureSqlDataTransmitter.Domain;
using DwdAzureSqlDataTransmitter.Services;
using DwdAzureSqlDataTransmitter.Services.Export;
using DwdAzureSqlDataTransmitter.Services.Import;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DwdAzureSqlDataTransmitter

{
    internal class Program
    {        
        static async Task Main(string[] args)
        {
            // Setting up the Entity Framework (sets the connection to the Azure DB) 
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AzureDbContext>(options =>
                        options.UseSqlServer(
                            // the Connetion String is defined in the appsettings.json
                            context.Configuration.GetConnectionString("AzureSql"),
                            sqlOptions =>
                            {
                                sqlOptions.EnableRetryOnFailure(
                                    maxRetryCount: 5,
                                    maxRetryDelay: TimeSpan.FromSeconds(10),
                                    errorNumbersToAdd: null);
                            }));
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

            int countImport = 0;

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
                            latestData.PrintProperties();
                            // converting the datatypes of the object and appending the StationId
                            // to prepare the Data for the upload
                            var record = DwdDtoConverter.Map(station.StationId, latestData);
                            dailyRecords.Add(record);
                        }
                    }

                    countImport++;                               

                    await Task.Delay(200); // optional rate limiting
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error for {station.StationKe}: {ex.Message}");
                }
            }
            // starts uploading to DB after all dailyRecords collected;
            // returns the number of rows added in the DB
            var countUpload = await exportService.ExportAsync(dailyRecords);

            Console.Clear();
            Console.WriteLine("ApiParameterFetcher");
            Console.WriteLine("------------------");

            Console.WriteLine(countImport + " Stations were succesfully parsed from DWD");
            Console.WriteLine(countUpload + " Stations were succesfully transmitted to the Database");

            Console.WriteLine("------------------");
            Console.Write("Press any Key to quit");

            Console.ReadKey();
        }
    }
}
