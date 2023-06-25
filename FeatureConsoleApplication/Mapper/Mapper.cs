using FeatureConsoleApplication.Dto;
using FeatureConsoleApplication.Models;

namespace FeatureConsoleApplication.Mapper
{
    /// <summary>
    /// Mapper for objects
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Mapps <see cref="EventDto"/> to <see cref="Event"/>
        /// </summary>
        /// <param name="eventDto"><see cref="EventDto"/> object</param>
        /// <returns><see cref="Event"/> object</returns>
        public static Event ToEvent(this EventDto eventDto) =>
            new()
            {
                Name = eventDto.Name,
                Event_Type = eventDto.Event_Type,
                Expires_At = eventDto.Expires_At,
                Starts_At = eventDto.Starts_At
            };

        /// <summary>
        /// Mapps <see cref="OfferDto"/> to <see cref="Offer"/>
        /// </summary>
        /// <param name="offerDto"><see cref="OfferDto"/> object</param>
        /// <returns><see cref="Offer"/> object</returns>
        public static Offer ToOffer(this OfferDto offerDto) =>
            new()
            {
                Name = offerDto.Name,
                Offer_Type = offerDto.Offer_Type,
                Expires_At = offerDto.Expires_At,
                Starts_At = offerDto.Starts_At
            };
    }
}
