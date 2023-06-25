using BusWebApi.Enums;

namespace BusWebApi.Models
{
    /// <summary>
    /// Offer entity
    /// </summary>
    public record Offer : DbModel, IAvailabilityModel
    {
        /// <summary>
        /// Offer type
        /// </summary>
        public OfferType Offer_Type { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Start to become available
        /// </summary>
        public DateTime Starts_At { get; set; }

        /// <summary>
        /// /// <summary>
        /// Stops to become available
        /// </summary>
        /// </summary>
        public DateTime Expires_At { get; set; }
    }
}
