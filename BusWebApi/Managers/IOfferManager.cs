using BusWebApi.Models;

namespace BusWebApi.Managers
{
    /// <summary>
    /// Interface for <see cref="OfferManager"/>
    /// </summary>
    public interface IOfferManager
    {
        /// <summary>
        /// Gets collection of active <see cref="Offer"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Offer"/>s</returns>
        IEnumerable<Offer> GetActiveOffers();

        /// <summary>
        /// Adds new record
        /// </summary>
        /// <param name="record">New <see cref="Offer"/> record object</param>
        void AddRecord(Offer record);
    }
}
