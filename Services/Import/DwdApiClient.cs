namespace ApiJsonSqlServer.Services.Import
{
    internal class DwdApiClient
    {
        private static readonly HttpClient _client = new HttpClient();
        public async Task<string> GetStationDataAsync(string stationKe)
        {
            string url = $"https://dwd.api.proxy.bund.dev/v30/stationOverviewExtended?stationIds={stationKe}";

            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
