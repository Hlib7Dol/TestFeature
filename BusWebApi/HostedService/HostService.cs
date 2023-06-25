using BusWebApi.Handlers;

namespace BusWebApi.HostedService
{
    /// <summary>
    /// Host service
    /// </summary>
    public class HostService : IHostedService
    {
        #region Private fields

        private readonly IDatabaseDataChangeHandler _changeHandler;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for <see cref="HostService"/>
        /// </summary>
        /// <param name="changeHandler">Storage data change handler</param>
        public HostService(IDatabaseDataChangeHandler changeHandler)
        {
            this._changeHandler = changeHandler;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Starts operation
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> for operation</param>
        /// <returns><see cref="Task"/></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this._changeHandler.StartTrackingChanges();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops operation
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> for operation</param>
        /// <returns><see cref="Task"/></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._changeHandler.StopTrackingChanges();

            return Task.CompletedTask;
        }

        #endregion
    }
}
