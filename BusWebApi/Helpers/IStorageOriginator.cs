using BusWebApi.Models;

namespace BusWebApi.Helpers
{
    /// <summary>
    /// Interface for <see cref="StorageOriginator"/>
    /// </summary>
    public interface IStorageOriginator
    {
        /// <summary>
        /// Events field
        /// </summary>
        IEnumerable<Event> Events { get; }

        /// <summary>
        /// Offers field
        /// </summary>
        IEnumerable<Offer> Offers { get; }

        /// <summary>
        /// Updates originator values from storage
        /// </summary>
        void UpdateOriginatorValues();

        /// <summary>
        /// Returns <see cref="IStorageMemento"/> object
        /// </summary>
        /// <returns><see cref="IStorageMemento"/> object</returns>
        IStorageMemento GetMemento();
    }
}
