using BusWebApi.Models;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using System.Text.Json;

namespace BusWebApi.DataAccessLayer
{
    public class RedisStorageAccessor : IStorageAccessor
    {
        private readonly IDatabase _database;

        public RedisStorageAccessor(IConfiguration configuration) 
        {
            var redisDbConnectionString = configuration.GetConnectionString("RedisDatabase");
            var redis = ConnectionMultiplexer.Connect(redisDbConnectionString);
            this._database = redis.GetDatabase();
        }

        public Task AddRecordAsync<T>(T value) where T : DbModel
        {
            var key = typeof(T).Name.ToLower();

            IJsonCommands json = this._database.JSON();

            var id = json.GetEnumerable<long>(key, $"$.{key}[*].Id").Max();
            value.Id = ++id;

            try
            {
                json.ArrAppend(key, path: $"$.{key}", value);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return Task.CompletedTask;
        }

        public async Task UpdateRecordsAsync<T>(IEnumerable<T> values) where T : DbModel
        {
            var key = typeof(T).Name.ToLower();

            var json = this._database.JSON();

            try
            {
                List<Task> tasks = new List<Task>();
                foreach (var item in values)
                {
                    var task = json.SetAsync(key, path: $"$.{key}[?(@.Id=={item.Id})]", item, When.Exists);

                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        public Task<IEnumerable<T>> GetUnsentItemsAsync<T>() where T : DbModel
        {
            var key = typeof(T).Name.ToLower();

            var json = this._database.JSON();

            IEnumerable<T?> items;
            try
            {
                items = json.GetEnumerable<T>(key, path: $"$.{key}[?(@.SendState==0)]");
            }
            catch(JsonException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return Task.FromResult(items);
        }

        public Task<IEnumerable<T?>> GetValuesFromSectionAsync<T>() where T : DbModel
        {
            var key = typeof(T).Name.ToLower();

            IJsonCommands json = this._database.JSON();

            IEnumerable<T?> results;
            try
            {
                results = json.GetEnumerable<T>(key, path: $"$.{key}[*]");
            }
            catch (JsonException ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return Task.FromResult(results);
        }
    }
}
