using BusWebApi.Enums;

namespace BusWebApi.Models
{
    /// <summary>
    /// Event entity
    /// </summary>
    public record Event : DbModel, IAvailabilityModel
    {
        /// <summary>
        /// Event type
        /// </summary>
        public EventType Event_Type { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Starts to become available
        /// </summary>
        public DateTime Starts_At { get; set; }

        /// <summary>
        /// Stops to become available
        /// </summary>
        public DateTime Expires_At { get; set; }
    }
}
