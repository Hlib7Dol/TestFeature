using BusWebApi.Enums;

namespace FeatureConsoleApplication.Models
{
    /// <summary>
    /// Event model
    /// </summary>
    public record Event
    {
        public EventType Event_Type { get; set; }

        public string Name { get; set; }

        public DateTime Starts_At { get; set; }

        public DateTime Expires_At { get; set; }
    }
}
