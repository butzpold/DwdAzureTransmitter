using DwdAzureSqlDataTransmitter.Domain;

// orchestrating AzureDbContext.cs and AzureDbClient.cs
namespace DwdAzureSqlDataTransmitter.Services.Export
{
    internal class AzureDbExportService
    {
        private readonly AzureDbClient _dbClient;

        public AzureDbExportService(AzureDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task<int> ExportAsync(List<WeatherRecord> records)
        {
            int countUpload = 0;

            var lastDates = await _dbClient.GetLastDatesAsync();
            // adding just the stations to newRecords, which lastDate not exists
            // in database (means the station doesn't exist at all) and lastDate 
            // is older than the date of the station in records
            var newRecords = records
                .Where(r => 
                    !lastDates.ContainsKey(r.StationId) || r.Date > lastDates[r.StationId])
                .ToList();
            // avoids unnecessary DB calls if there isn't any newRecords to insert
            if (newRecords.Any())
            {
                // InsertRangeAsync() returns how many rows EF Core actually wrote                 
                countUpload += await _dbClient.InsertRangeAsync(newRecords);
            }
        return countUpload;
        }
    }
}
