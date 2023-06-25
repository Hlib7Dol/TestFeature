namespace BusWebApi.Models
{
    /// <summary>
    /// Interface that desribes availability of the object
    /// </summary>
    public interface IAvailabilityModel
    {
        /// <summary>
        /// Start to become available
        /// </summary>
        DateTime Starts_At { get; set; }

        /// <summary>
        /// Stops to become available
        /// </summary>
        DateTime Expires_At { get; set; }
    }
}
