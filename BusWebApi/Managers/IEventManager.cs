using BusWebApi.Models;

namespace BusWebApi.Managers
{
    /// <summary>
    /// Interface for <see cref="EventManager"/>
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Gets collection of active <see cref="Event"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Event"/>s</returns>
        IEnumerable<Event> GetActiveEvents();

        /// <summary>
        /// Adds new record
        /// </summary>
        /// <param name="record">New <see cref="Event"/> record object</param>
        void AddRecord(Event record);
    }
}
