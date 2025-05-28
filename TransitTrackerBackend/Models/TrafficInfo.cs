namespace TransitTracker.TrafficAPI.Models
{
    public class TrafficInfo
    {
        public string Line { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public long WaitingTimeMinutes { get; set; }
    }
}