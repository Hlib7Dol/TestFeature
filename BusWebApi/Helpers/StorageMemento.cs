using BusWebApi.Models;

namespace BusWebApi.Helpers
{
    /// <summary>
    /// Storage memento
    /// </summary>
    public class StorageMemento : IStorageMemento
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> of <see cref="Event"/>s
        /// </summary>
        public IEnumerable<Event> Events { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> of <see cref="Offer"/>s
        /// </summary>
        public IEnumerable<Offer> Offers { get; set; }
    }
}
