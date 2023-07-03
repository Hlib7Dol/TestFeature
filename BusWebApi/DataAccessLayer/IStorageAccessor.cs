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
        Task AddRecordAsync<T>(T value) where T : DbModel;

        /// <summary>
        /// Gets collection of unsent items
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <returns>Collection of section items</returns>
        Task<IEnumerable<T>> GetUnsentItemsAsync<T>() where T : DbModel;

        /// <summary>
        /// Updates records in the storage
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <param name="values">Collection of <see cref="DbModel"/>s</param>
        Task UpdateRecordsAsync<T>(IEnumerable<T> values) where T : DbModel;

        /// <summary>
        /// Gets values from storage section
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <returns>Collection of section items</returns>
        Task<IEnumerable<T>> GetValuesFromSectionAsync<T>() where T : DbModel;
    }
}
