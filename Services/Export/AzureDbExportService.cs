using ApiJsonSqlServer.Domain;

// orchestrating AzureDbContext.cs and AzureDbClient.cs
namespace ApiJsonSqlServer.Services.Export
{
    internal class AzureDbExportService
    {
        private readonly AzureDbClient _dbClient;

        public AzureDbExportService(AzureDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        public async Task ExportAsync(List<WeatherRecord> records)
        {
            foreach (var stationGroup in records.GroupBy(r => r.StationId))
            {
                var lastDate = await _dbClient.GetLastDataAsync(stationGroup.Key);
                var newRecords = stationGroup
                    .Where(r => lastDate == null || r.Date > lastDate);

                await _dbClient.InsertRangeAsync(newRecords);
            }
        }
    }
}
