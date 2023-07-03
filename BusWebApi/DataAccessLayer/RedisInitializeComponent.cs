using NRedisStack.RedisStackCommands;
using NRedisStack;
using StackExchange.Redis;

namespace BusWebApi.DataAccessLayer
{
    public class RedisInitializeComponent
    {
        private readonly IDatabase _database;

        public RedisInitializeComponent()
        {
            var redis = ConnectionMultiplexer.Connect("redis-15027.c300.eu-central-1-1.ec2.cloud.redislabs.com:15027,password=R83AUmrEUA3n9TNQj2KuvJhQyZQDqwQO");
            _database = redis.GetDatabase();

            InitializeRedis();
        }

        private void InitializeRedis()
        {
            IJsonCommands json = _database.JSON();

            var key = "event";
            var keyOffer = "offer";
            json.Del(key);
            json.Set(key, "$", "{\"event\": []}");
            json.Del(keyOffer);
            json.Set(keyOffer, "$", "{\"offer\": []}");

            // Update table items
            var keyUpdateItems = "updateItems";
            json.Del(keyUpdateItems);
            json.Set(key, "$", "{\"event\": [], \"offer\": []}");
        }
    }
}
