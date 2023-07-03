using BusWebApi.Helpers;
using BusWebApi.Models;
using BusWebApi.Services;
using Microsoft.Azure.SignalR.Common;

namespace BusWebApi.Handlers
{
    /// <summary>
    /// Storage data change handler
    /// </summary>
    public class DatabaseDataChangeHandler : IDatabaseDataChangeHandler
    {
        #region Private fields

        private readonly IStorageOriginator _storageOriginator;
        private readonly IStorageCaretaker _storageCaretaker;
        private readonly ISignalRMessageSenderService _signalRMessageSenderService;
        private readonly IDifferenceTrackerService _differenceService;
        private readonly FileSystemWatcher _fileSystemWatcher;

        private readonly string _storageFolderPath;

        private const string StorageFileName = "storage.json";

        private readonly IDictionary<Type, string> _routes = new Dictionary<Type, string>()
        {
            { typeof(Event), "event" },
            { typeof(Offer), "offer" }
        };

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="DatabaseDataChangeHandler"/>
        /// </summary>
        /// <param name="signalRMessageSenderService">Signal R message sender service</param>
        /// <param name="repository">Storage data context</param>
        /// <param name="storageCaretaker">Storage memento</param>
        /// <param name="differenceService">Difference tracker service</param>
        /// <param name="configuration">Application configuration properties</param>
        public DatabaseDataChangeHandler(ISignalRMessageSenderService signalRMessageSenderService,
            IStorageOriginator repository,
            IStorageCaretaker storageCaretaker,
            IDifferenceTrackerService differenceService,
            IConfiguration configuration)
        {
            this._differenceService = differenceService;
            this._signalRMessageSenderService = signalRMessageSenderService;
            this._storageOriginator = repository;
            this._storageCaretaker = storageCaretaker;
            this._storageFolderPath = $"{Directory.GetCurrentDirectory()}\\{configuration["StorageFolderPath"]}";
            this._fileSystemWatcher = new FileSystemWatcher();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts tracking database changes
        /// </summary>
        public Task StartTrackingChangesAsync()
        {
            _fileSystemWatcher.Path = this._storageFolderPath;
            _fileSystemWatcher.Changed += TrackChangesAsync;
            _fileSystemWatcher.EnableRaisingEvents = true;

            this._storageOriginator.UpdateOriginatorValuesAsync();
            this._storageCaretaker.Memento = this._storageOriginator.GetMemento();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops tracking database changes
        /// </summary>
        public Task StopTrackingChangesAsync()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
            _fileSystemWatcher.Changed -= TrackChangesAsync;
            _fileSystemWatcher.Dispose();

            return Task.CompletedTask;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Tracks storage changes
        /// </summary>
        /// <param name="s">Event object</param>
        /// <param name="e">Event arguments</param>
        /// <exception cref="NullReferenceException"></exception>
        private async void TrackChangesAsync(object s, FileSystemEventArgs e) // it better to be Task return type, but file watcher changed event is subscribed to this method
        {
            if(e.Name != StorageFileName)
            {
                return;
            }

            this._storageOriginator.UpdateOriginatorValuesAsync();

            HashSet<IAvailabilityModel> models = new();

            var eventsChanges = this._differenceService.GetDifference(this._storageOriginator.Events, this._storageCaretaker.Memento.Events);
            if(eventsChanges.Any())
            {
                models.UnionWith(eventsChanges);
            }

            var offerChanges = this._differenceService.GetDifference(this._storageOriginator.Offers, this._storageCaretaker.Memento.Offers);
            if(offerChanges.Any())
            {
                models.UnionWith(offerChanges);
            }

            if(!models.Any())
            {
                return;
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
                    }
                    catch (FailedWritingMessageToServiceException ex)
                    {
                        Console.WriteLine($"SignalR disconnected, unable to send message, {ex.Message}");
                    }
                }
            }

            Validate(models);

            await SendAsync(models);

            this._storageCaretaker.Memento = this._storageOriginator.GetMemento();
        }

        #endregion
    }
}
