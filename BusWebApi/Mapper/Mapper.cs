using BusWebApi.Models;
using BusWebApi.DTO;

namespace BusWebApi.Mapper
{
    /// <summary>
    /// Mapper for objects
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Mapps from <see cref="EventDto"/> to <see cref="Event"/>
        /// </summary>
        /// <param name="eventDto"><see cref="EventDto"/> obj</param>
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
        /// Mapps from <see cref="Event"/> to <see cref="EventDto"/>
        /// </summary>
        /// <param name="eventDto"><see cref="Event"/> obj</param>
        /// <returns><see cref="EventDto"/> object</returns>
        public static EventDto ToEventDto(this Event eventObj) =>
            new()
            {
                Name = eventObj.Name,
                Expires_At = eventObj.Expires_At,
                Starts_At = eventObj.Starts_At,
                Event_Type = eventObj.Event_Type
            };

        /// <summary>
        /// Mapps from <see cref="OfferDto"/> to <see cref="Offer"/>
        /// </summary>
        /// <param name="offerDto"><see cref="OfferDto"/> obj</param>
        /// <returns><see cref="Offer"/> object</returns>
        public static Offer ToOffer(this OfferDto offerDto) =>
            new()
            {
                Name = offerDto.Name,
                Offer_Type = offerDto.Offer_Type,
                Expires_At = offerDto.Expires_At,
                Starts_At = offerDto.Starts_At
            };

        /// <summary>
        /// Mapps from <see cref="Offer"/> to <see cref="OfferDto"/>
        /// </summary>
        /// <param name="offerDto"><see cref="Offer"/> obj</param>
        /// <returns><see cref="OfferDto"/> object</returns>
        public static OfferDto ToOfferDto(this Offer offerDto) =>
            new()
            {
                Name = offerDto.Name,
                Expires_At = offerDto.Expires_At,
                Starts_At = offerDto.Starts_At,
                Offer_Type = offerDto.Offer_Type
            };
    }
}
