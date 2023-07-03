namespace BusWebApi.Handlers
{
    /// <summary>
    /// Interface for <see cref="DatabaseDataChangeHandler"/>
    /// </summary>
    public interface IDatabaseDataChangeHandler
    {
        /// <summary>
        /// Starts tracking changes of the storage
        /// </summary>
        Task StartTrackingChangesAsync();

        /// <summary>
        /// Stops tracking changes of the storage
        /// </summary>
        Task StopTrackingChangesAsync();
    }
}
