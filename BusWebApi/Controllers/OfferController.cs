using BusWebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using BusWebApi.Mapper;
using BusWebApi.Managers;

namespace Shared.Controllers
{
    /// <summary>
    /// Offer Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        #region Private fields

        private readonly IOfferManager _offerManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="OfferController"/>
        /// </summary>
        /// <param name="offerManager">Offer manager <see cref="IOfferManager"/></param>
        public OfferController(IOfferManager offerManager)
        {
            this._offerManager = offerManager;
        }

        #endregion

        #region Post

        /// <summary>
        /// Adds new offer
        /// </summary>
        /// <param name="offerDto">Offer dto object</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="offerDto"/> is null</exception>
        [HttpPost]
        public void AddOffer(OfferDto offerDto)
        {
            if(offerDto == null)
            {
                throw new ArgumentNullException(nameof(offerDto));
            }

            this._offerManager.AddRecord(offerDto.ToOffer());
        }

        #endregion

        #region Get

        /// <summary>
        /// Returns collection of active offers
        /// </summary>
        /// <returns>Collections of <see cref="OfferDto"/>s</returns>
        [HttpGet]
        [Route("GetActiveOffers")]
        public IEnumerable<OfferDto> GetActiveOffers(CancellationToken cancellationToken = default)
        {
            var activeEvents = this._offerManager.GetActiveOffers();

            return activeEvents.Select(x => x.ToOfferDto());
        }

        #endregion
    }
}
