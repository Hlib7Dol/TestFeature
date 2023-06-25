using BusWebApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FeatureConsoleApplication.Dto
{
    public record OfferDto
    {
        [Required, NotNull]
        public string Name { get; set; }

        [Required]
        public DateTime Starts_At { get; set; }

        [Required]
        public DateTime Expires_At { get; set; }

        [Required]
        public OfferType Offer_Type { get; set; }
    }
}
