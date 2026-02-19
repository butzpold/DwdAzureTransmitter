using DwdAzureSqlDataTransmitter.Domain;
using System.Globalization;

namespace DwdAzureSqlDataTransmitter.Services
{
    internal class CsvReader
    {
        public static List<StationMetaData> Read(string filepath)
        {
            var rows = File.ReadAllLines(filepath);
            var stations = new List<StationMetaData>();

            foreach (var line in rows.Skip(1)) // skip header
            {
                var columns = line.Split(';'); // adjust separator

                string name = columns[0].Trim();
                if (!int.TryParse(columns[1], out int id))
                    continue;
                string ke = columns[2];
                if (!double.TryParse(columns[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double latit))
                    continue;
                if (!double.TryParse(columns[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double longit))
                    continue;
                if (!int.TryParse(columns[5], out int altit))
                    continue;

                stations.Add(
                    new StationMetaData
                    {
                        StationName = name,
                        StationId = id,
                        StationKe = ke,
                        StationLatitude = latit,
                        StationLongitude = longit,
                        StationAltitude = altit
                    }
                );
            }
            return stations;
        }
    }
}
