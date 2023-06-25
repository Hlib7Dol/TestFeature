using Microsoft.AspNetCore.Mvc;
using BusWebApi.DTO;
using BusWebApi.Mapper;
using BusWebApi.Managers;

namespace BusWebApi.Controllers
{
    /// <summary>
    /// Event controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        #region private fields

        private readonly IEventManager _eventManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor for <see cref="EventController"/>
        /// </summary>
        /// <param name="eventManager">Event manager <see cref="IEventManager"/></param>
        public EventController(IEventManager eventManager)
        {
            this._eventManager = eventManager;
        }

        #endregion

        #region Post

        /// <summary>
        /// Adds new event
        /// </summary>
        /// <param name="addEventDto">Add event dto</param>
        /// <exception cref="ArgumentNullException">Thrown if event dto is null</exception>
        [HttpPost]
        public void AddEvent(EventDto addEventDto)
        {
            if(addEventDto == null)
            {
                throw new ArgumentNullException(nameof(addEventDto));
            }

            this._eventManager.AddRecord(addEventDto.ToEvent());
        }

        #endregion

        #region Get

        /// <summary>
        /// Returns collection of active events
        /// </summary>
        /// <returns>Collection of <see cref="EventDto"/>s</returns>
        [HttpGet]
        [Route("GetActiveEvents")]
        public IEnumerable<EventDto> GetActiveEvents(CancellationToken cancellationToken = default) 
        {
            var activeEvents = this._eventManager.GetActiveEvents();

            return activeEvents.Select(x => x.ToEventDto());
        }

        #endregion
    }
}
