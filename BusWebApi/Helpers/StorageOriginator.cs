using BusWebApi.Models;
using BusWebApi.DataAccessLayer;

namespace BusWebApi.Helpers
{
    /// <summary>
    /// Storage originator
    /// </summary>
    public class StorageOriginator : IStorageOriginator
    {
        #region Public fields

        /// <summary>
        /// Events field
        /// </summary>
        public IEnumerable<Event> Events { get; private set; }

        /// <summary>
        /// Offers field
        /// </summary>
        public IEnumerable<Offer> Offers { get; private set; }

        #endregion

        #region Private fields

        private readonly IStorageAccessor _storageAccessor;

        private readonly IStorageMemento _storageMemento;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="StorageOriginator"/>
        /// </summary>
        /// <param name="storageAccessor">Storage accessor</param>
        public StorageOriginator(IStorageAccessor storageAccessor, IStorageMemento storageMemento)
        {
            this._storageAccessor = storageAccessor;
            this._storageMemento = storageMemento;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates originator values from storage
        /// </summary>
        public void UpdateOriginatorValues()
        {
            Events = _storageAccessor.GetValuesFromSection<Event>();
            Offers = _storageAccessor.GetValuesFromSection<Offer>();
        }

        /// <summary>
        /// Returns <see cref="IStorageMemento"/> object
        /// </summary>
        /// <returns><see cref="IStorageMemento"/> object</returns>
        public IStorageMemento GetMemento()
        {
            this._storageMemento.Events = this.Events;
            this._storageMemento.Offers = this.Offers;

            return _storageMemento;
        }

        #endregion
    }
}
