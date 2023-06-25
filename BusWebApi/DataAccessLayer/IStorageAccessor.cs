using BusWebApi.Models;

namespace BusWebApi.DataAccessLayer
{
    /// <summary>
    /// Interface for StorageAccessor, which provides acces to the storage
    /// </summary>
    public interface IStorageAccessor
    {
        /// <summary>
        /// Adds new record to the storage
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <param name="value">Object that will be added to the database</param>
        void AddRecord<T>(T value) where T : DbModel;

        /// <summary>
        /// Gets values from storage section
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <returns>Collection of section items</returns>
        IEnumerable<T> GetValuesFromSection<T>() where T : DbModel;
    }
}
