using BusWebApi.Models;
using BusWebApi.DataAccessLayer;

namespace BusWebApi.Managers
{
    /// <summary>
    /// Event manager
    /// </summary>
    public class EventManager : IEventManager
    {
        #region Private fields

        private readonly IStorageAccessor _storageAccessor;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="EventManager"/>
        /// </summary>
        /// <param name="storageAccessor">Storage accesor</param>
        public EventManager(IStorageAccessor storageAccessor)
        {
            this._storageAccessor = storageAccessor;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds new record
        /// </summary>
        /// <param name="record">New <see cref="Event"/> record object</param>
        public async Task AddRecord(Event record)
        {
            await this._storageAccessor.AddRecordAsync(record);
        }

        /// <summary>
        /// Gets collection of active <see cref="Event"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Event"/>s</returns>
        public async Task<IEnumerable<Event>> GetActiveEvents()
        {
            var events = await this._storageAccessor.GetValuesFromSectionAsync<Event>();

            if(events == null)
            {
                return Enumerable.Empty<Event>();
            }

            var activeEvents = events.Where(x => x.Starts_At < DateTime.UtcNow && x.Expires_At > DateTime.UtcNow);

            return activeEvents;
        }

        #endregion
    }
}
