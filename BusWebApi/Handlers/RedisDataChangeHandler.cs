using BusWebApi.DataAccessLayer;
using BusWebApi.Models;
using BusWebApi.Services;
using Microsoft.Azure.SignalR.Common;
using StackExchange.Redis;

namespace BusWebApi.Handlers
{
    public class RedisDataChangeHandler : IDatabaseDataChangeHandler
    {
        private readonly ISubscriber _subscriber;
        private readonly ISignalRMessageSenderService _signalRMessageSenderService;
        private readonly IStorageAccessor _redisStorageAccessor;

        private const string JsonArrAppend = "json.arrappend";

        private readonly IDictionary<Type, string> _routes = new Dictionary<Type, string>()
        {
            { typeof(Event), "event" },
            { typeof(Offer), "offer" }
        };

        public RedisDataChangeHandler(ISignalRMessageSenderService signalRMessageSenderService,
            IStorageAccessor redisStorageAccessor,
            IConfiguration configuration) 
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisDatabase"));
            this._subscriber = connection.GetSubscriber();
            this._signalRMessageSenderService = signalRMessageSenderService;
            this._redisStorageAccessor = redisStorageAccessor;
        }

        public async Task StartTrackingChangesAsync()
        {
            await this._subscriber.SubscribeAsync("__keyspace@0__:offer", async (c, v) => await HandleAsync(c, v));
            await this._subscriber.SubscribeAsync("__keyspace@0__:event", async (c, v) => await HandleAsync(c, v));
        }

        public async Task StopTrackingChangesAsync()
        {
            await this._subscriber.UnsubscribeAllAsync();
        }

        private async Task HandleAsync(RedisChannel redisChannel, RedisValue redisValue)
        {
            if(redisValue != JsonArrAppend)
            {
                return;
            }

            var type = this.GetNotificationTableType(redisChannel);

            IEnumerable<IAvailabilityModel> models;
            if(type == "event")
            {
                models = await this._redisStorageAccessor.GetUnsentItemsAsync<Event>();
            }
            else if(type == "offer")
            {
                models = await this._redisStorageAccessor.GetUnsentItemsAsync<Offer>();
            }
            else
            {
                throw new ArgumentException($"{type} is not supported.");
            }

            void Validate(IEnumerable<IAvailabilityModel> models)
            {
                if (models == null)
                {
                    throw new NullReferenceException("Data is null");
                }

                models = models.Where(x => x.Starts_At < DateTime.UtcNow && x.Expires_At > DateTime.UtcNow);
            }

            async Task SendAsync(IEnumerable<IAvailabilityModel> data)
            {
                foreach (var item in data)
                {
                    try
                    {
                        await _signalRMessageSenderService.SendAsync(_routes[item.GetType()], item);

                        (item as DbModel).SendState = Enum.SyncState.Sent;
                    }
                    catch (FailedWritingMessageToServiceException ex)
                    {
                        Console.WriteLine($"SignalR disconnected, unable to send message, {ex.Message}");
                    }
                }
            }

            Validate(models);

            await SendAsync(models);

            await this._redisStorageAccessor.UpdateRecordsAsync(models as IEnumerable<DbModel>);
        }

        private string GetNotificationTableType(RedisChannel redisChannel)
        {
            var typeName = redisChannel.ToString().Split(":")[1];

            return typeName;
        }
    }
}
