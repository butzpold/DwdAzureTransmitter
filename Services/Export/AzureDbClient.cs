using Microsoft.EntityFrameworkCore;
using ApiJsonSqlServer.Domain;

namespace ApiJsonSqlServer.Services.Export
{
    public class AzureDbClient
    {
        private readonly AzureDbContext _context;

        public AzureDbClient(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<DateOnly?> GetLastDataAsync(int stationId)
        {
            return await _context.WeatherRecords
                .Where(w => w.StationId == stationId)
                .MaxAsync(w => (DateOnly?)w.Date);
        }

        public async Task InsertRangeAsync(IEnumerable<WeatherRecord> records)
        {
            _context.WeatherRecords.AddRange(records);
            await _context.SaveChangesAsync();
        }
    }
}
