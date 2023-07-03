using BusWebApi.Models;
using BusWebApi.DataAccessLayer;

namespace BusWebApi.Managers
{
    /// <summary>
    /// Offer manager
    /// </summary>
    public class OfferManager : IOfferManager
    {
        #region Private fields

        private readonly IStorageAccessor _storageAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="OfferManager"/>
        /// </summary>
        /// <param name="storageAccessor">Storage accessor</param>
        public OfferManager(IStorageAccessor storageAccessor) 
        {
            this._storageAccessor = storageAccessor;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds new record
        /// </summary>
        /// <param name="record">New <see cref="Offer"/> record object</param>
        public async Task AddRecord(Offer record)
        {
            await this._storageAccessor.AddRecordAsync(record);
        }

        /// <summary>
        /// Gets collection of active <see cref="Offer"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Offer"/>s</returns>
        public async Task<IEnumerable<Offer>> GetActiveOffers()
        {
            var offers = await this._storageAccessor.GetValuesFromSectionAsync<Offer>();

            if (offers == null)
            {
                return Enumerable.Empty<Offer>();
            }

            var activeOffers = offers.Where(x => x.Starts_At <  DateTime.UtcNow && x.Expires_At > DateTime.UtcNow);

            return activeOffers;
        }

        #endregion
    }
}
