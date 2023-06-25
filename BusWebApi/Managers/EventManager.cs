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
        public void AddRecord(Event record)
        {
            this._storageAccessor.AddRecord(record);
        }

        /// <summary>
        /// Gets collection of active <see cref="Event"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Event"/>s</returns>
        public IEnumerable<Event> GetActiveEvents()
        {
            var events = this._storageAccessor.GetValuesFromSection<Event>();

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
