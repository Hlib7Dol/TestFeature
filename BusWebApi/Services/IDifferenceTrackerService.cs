namespace BusWebApi.Services
{
    /// <summary>
    /// Interface for <see cref="DifferenceTrackerService"/>
    /// </summary>
    public interface IDifferenceTrackerService
    {
        /// <summary>
        /// Gets the difference between two <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">Any kind of parameter</typeparam>
        /// <param name="original">Original <see cref="IEnumerable{T}"/> object</param>
        /// <param name="another">Another <see cref="IEnumerable{T}"/> object</param>
        /// <returns><see cref="IEnumerable{T}"/> of differences of two collections</returns>
        IEnumerable<T> GetDifference<T>(IEnumerable<T> original, IEnumerable<T> another);
    }
}