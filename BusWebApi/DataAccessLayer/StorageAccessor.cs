using BusWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusWebApi.DataAccessLayer
{
    /// <summary>
    /// Storage accessor, which provides access to the storage
    /// </summary>
    public class StorageAccessor : IStorageAccessor
    {
        #region Private fields

        private readonly string _storagePath;
        private static readonly object _lockObject = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="StorageAccessor"/>
        /// </summary>
        /// <param name="configuration">Application configuration properties</param>
        public StorageAccessor(IConfiguration configuration)
        {
            var storagePath = $"{Directory.GetCurrentDirectory()}\\{configuration["StoragePath"]}";
            _storagePath = storagePath;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets values from storage section
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <returns>Collection of section values</returns>
        /// <exception cref="ArgumentNullException">Thrown if section is not present</exception>
        public Task<IEnumerable<T>> GetValuesFromSectionAsync<T>() where T : DbModel
        {
            lock (_lockObject)
            {
                using var streamReader = new StreamReader(_storagePath);
                var json = streamReader.ReadToEnd();

                JObject jsonObject;
                try
                {
                    jsonObject = JObject.Parse(json);
                }
                catch(JsonReaderException e)
                {
                    Console.WriteLine($"Cannot parse to json string, GetValuesFromSection method, {e.Message}");

                    throw e;
                }

                var sectionName = typeof(T).Name.ToLower();
                var section = jsonObject[sectionName]?.ToString() ?? throw new ArgumentNullException($"No such section {sectionName}");

                IEnumerable<T> result;
                try
                {
                    result = System.Text.Json.JsonSerializer.Deserialize<List<T>>(section);
                }
                catch(System.Text.Json.JsonException ex)
                {
                    Console.WriteLine($"Not supported json to deserialize, {ex.Message}, GetValuesFromSection method, {sectionName} section");

                    throw ex;
                }

                return Task.FromResult(result);
            }
        }

        /// <summary>
        /// Adds new record to the storage
        /// </summary>
        /// <typeparam name="T">Parameter of type <see cref="DbModel"/></typeparam>
        /// <param name="value">Object that will be added to the database</param>
        /// <exception cref="ArgumentNullException">Thrown if section is not present</exception>
        public Task AddRecordAsync<T>(T value) where T : DbModel
        {
            lock (_lockObject)
            {
                JObject jsonObject;
                using (var streamReader = new StreamReader(_storagePath))
                {
                    var json = streamReader.ReadToEnd();

                    try
                    {
                        jsonObject = JObject.Parse(json);
                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine($"Cannot parse to json string, GetValuesFromSection method, {e.Message}");

                        throw e;
                    }

                    var sectionName = typeof(T).Name.ToLower();
                    var section = jsonObject[sectionName]?.ToString() ?? throw new ArgumentException($"Wrong object type {sectionName}");

                    List<T> tList = null;
                    try
                    {
                        tList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(section);
                    }
                    catch (System.Text.Json.JsonException ex)
                    {
                        Console.WriteLine($"Not supported json to deserialize, {ex.Message}, AddRecord method, {sectionName} section");

                        throw ex;
                    }

                    var lastId = tList!.LastOrDefault()?.Id;
                    value.Id = lastId.HasValue ? lastId.Value + 1 : 1;
                    tList!.Add(value);

                    var serialized = System.Text.Json.JsonSerializer.Serialize(tList);
                    jsonObject[sectionName] = serialized;
                }

                using var streamWriter = new StreamWriter(_storagePath);

                streamWriter.Write(jsonObject);
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetUnsentItemsAsync<T>() where T : DbModel
        {
            return Task.FromResult<IEnumerable<T>>(null);
        }

        public Task UpdateRecordsAsync<T>(IEnumerable<T> values) where T : DbModel
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
