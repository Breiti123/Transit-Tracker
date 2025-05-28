using System.Text.Json;
using TransitTracker.TrafficAPI.Models;

namespace TransitTracker.TrafficAPI.Services
{
    public class MockTrafficService : ITrafficService
    {
        private readonly string _filePath = "Data/mock_traffic.json";

        public MockTrafficService()
        {
            if (!File.Exists(_filePath))
            {
                var random = Math.Abs(Random.Shared.NextInt64((1L << 52)));

                var mockData = new List<TrafficInfo>
                {
                    new() { Line = "U1", From = "Reumannplatz", To = "Stephansplatz", WaitingTimeMinutes = (int)(random % 10 + 1) },
                    new() { Line = "13A", From = "Hauptbahnhof", To = "Alser Straße", WaitingTimeMinutes = (int)(random % 5 + 2) },
                    new() { Line = "A1", From = "Linz", To = "St. Pölten", WaitingTimeMinutes = (int)(random % 13 + 8) },
                    new() { Line = "A2", From = "Wien", To = "Graz", WaitingTimeMinutes = (int)(random % 21 + 25) }
                };

                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
                File.WriteAllText(_filePath, JsonSerializer.Serialize(mockData, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public async Task<IEnumerable<TrafficInfo>> GetTrafficAsync()
        {
            using var stream = File.OpenRead(_filePath);
            var data = await JsonSerializer.DeserializeAsync<List<TrafficInfo>>(stream);
            return data ?? Enumerable.Empty<TrafficInfo>();
        }
    }
}



