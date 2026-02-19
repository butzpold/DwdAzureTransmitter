namespace DwdAzureSqlDataTransmitter.Domain
{
    internal class StationMetaData
    {
        public string? StationName { get; set; }
        public int StationId { get; set; }
        public string? StationKe { get; set; }
        public double StationLatitude { get; set; }
        public double StationLongitude { get; set; }
        public double StationAltitude { get; set; }
    }
}

