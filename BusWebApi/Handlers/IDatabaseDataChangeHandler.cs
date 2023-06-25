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
        void StartTrackingChanges();

        /// <summary>
        /// Stops tracking changes of the storage
        /// </summary>
        void StopTrackingChanges();
    }
}
