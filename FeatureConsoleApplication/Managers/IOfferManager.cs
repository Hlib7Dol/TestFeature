using FeatureConsoleApplication.Models;

namespace FeatureConsoleApplication.Managers
{
    /// <summary>
    /// Interface for <see cref="OfferManager"/>
    /// </summary>
    internal interface IOfferManager
    {
        /// <summary>
        /// Gets active <see cref="Offer"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Offer"/>s</returns>
        Task<IEnumerable<Offer>> GetActiveOffersAsync();
    }
}
