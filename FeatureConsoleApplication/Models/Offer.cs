using BusWebApi.Enums;

namespace FeatureConsoleApplication.Models
{
    /// <summary>
    /// Offer model
    /// </summary>
    public record Offer
    {
        public OfferType Offer_Type { get; set; }

        public string Name { get; set; }

        public DateTime Starts_At { get; set; }

        public DateTime Expires_At { get; set; }
    }
}
