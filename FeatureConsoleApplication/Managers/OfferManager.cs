using FeatureConsoleApplication.Dto;
using FeatureConsoleApplication.Mapper;
using FeatureConsoleApplication.Models;
using FeatureConsoleApplication.Services;

namespace FeatureConsoleApplication.Managers
{
    /// <summary>
    /// Offer manager
    /// </summary>
    internal class OfferManager : IOfferManager
    {
        #region Private methods

        private readonly IRequestService _requestService;
        private const string GetActiveOffersUrl = "Offer/GetActiveOffers";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="OfferManager"/>
        /// </summary>
        public OfferManager()
        {
            this._requestService = new RequestService();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets active <see cref="Offer"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Offer"/>s</returns>
        public async Task<IEnumerable<Offer>> GetActiveOffersAsync()
        {
            var response = await this._requestService.GetAsync<List<OfferDto>>(GetActiveOffersUrl);

            return response.Select(x => x.ToOffer());
        }

        #endregion
    }
}
