using BusWebApi.Models;

namespace BusWebApi.Helpers
{
    /// <summary>
    /// Interface for <see cref="StorageMemento"/>
    /// </summary>
    public interface IStorageMemento
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> of <see cref="Event"/>s
        /// </summary>
        IEnumerable<Event>  Events { get; set; }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> of <see cref="Offer"/>s
        /// </summary>
        IEnumerable<Offer> Offers { get; set; }
    }
}
