using Microsoft.EntityFrameworkCore;
using DwdAzureSqlDataTransmitter.Domain;

namespace DwdAzureSqlDataTransmitter.Services.Export
{
    public class AzureDbClient
    {
        private readonly AzureDbContext _context;

        public AzureDbClient(AzureDbContext context)
        {
            _context = context;
        }
        // queries th DB;
        // returns the latestDate or NULL if station not exists
        // of each station in DB in a Dictionnary 
        // equivalent SQL:
        // SELECT StationId, MAX(Date) FROM WeatherRecords GROUP BY StationId
        public async Task<Dictionary<int, DateOnly>> GetLastDatesAsync()
        {
            return await _context.WeatherRecords
                .GroupBy(w => w.StationId)
                .Select(g => new
                {
                    StationId = g.Key,
                    LastDate = g.Max(w => w.Date)
                })
                .ToDictionaryAsync(x => x.StationId, x => x.LastDate);
        }
        public async Task<int> InsertRangeAsync(IEnumerable<WeatherRecord> records)
        {
            // Adds all entities to EF’s ChangeTracker & 
            // marks them as "Added"; no DB call yet
            _context.WeatherRecords.AddRange(records);
            // generates the SQL INSERT statements,
            // sends them to database & executes them;
            // returns number of inserted rows
            return await _context.SaveChangesAsync();
        }
    }
}
