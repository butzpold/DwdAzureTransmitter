using Microsoft.EntityFrameworkCore;
using ApiJsonSqlServer.Domain;

namespace ApiJsonSqlServer.Services.Export
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> options)
            : base(options)
        {
        }
        // DbSet declares in which table (WeatherRecords)
        // the Data(WeatherRecord) should be appended   
        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definition of the Primary Keys
            modelBuilder.Entity<WeatherRecord>()
                .HasKey(w => new { w.StationId, w.Date });
            // Definition of the considering Properties for appending
            modelBuilder.Entity<WeatherRecord>()
                .Property(w => w.Date)
                .HasColumnName("Date");
        }
    }
}
