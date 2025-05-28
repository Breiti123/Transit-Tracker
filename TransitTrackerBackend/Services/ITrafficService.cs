using TransitTracker.TrafficAPI.Models;

namespace TransitTracker.TrafficAPI.Services
{
    public interface ITrafficService
    {
        Task<IEnumerable<TrafficInfo>> GetTrafficAsync();
    }
}


