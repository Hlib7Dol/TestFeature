using FeatureConsoleApplication.Dto;
using FeatureConsoleApplication.Mapper;
using FeatureConsoleApplication.Models;
using FeatureConsoleApplication.Services;

namespace FeatureConsoleApplication.Managers
{
    /// <summary>
    /// Event manager
    /// </summary>
    internal class EventManager : IEventManager
    {
        #region Private fields

        private readonly IRequestService _requestService;
        private const string GetActiveEventsUrl = "Event/GetActiveEvents";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="EventManager"/>
        /// </summary>
        public EventManager() 
        {
            this._requestService = new RequestService();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets active <see cref="Event"/>s
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Event"/>s</returns>
        public async Task<IEnumerable<Event>> GetActiveEventsAsync()
        {
            var response = await this._requestService.GetAsync<List<EventDto>>(GetActiveEventsUrl);

            return response.Select(x => x.ToEvent());
        }

        #endregion
    }
}
