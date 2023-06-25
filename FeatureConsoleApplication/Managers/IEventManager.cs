using FeatureConsoleApplication.Models;

namespace FeatureConsoleApplication.Managers
{
    /// <summary>
    /// Interface for <see cref="EventManager"/>
    /// </summary>
    internal interface IEventManager
    {
        /// <summary>
        /// Gets active <see cref="Event"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Event"/>s</returns>
        Task<IEnumerable<Event>> GetActiveEventsAsync();
    }
}
