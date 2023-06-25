namespace BusWebApi.Services
{
    /// <summary>
    /// SignalR message sender service
    /// </summary>
    public interface ISignalRMessageSenderService
    {
        /// <summary>
        /// Sends message to Azure SignalR service
        /// </summary>
        /// <typeparam name="T">Any type of parameter</typeparam>
        /// <param name="method">Topic method name</param>
        /// <param name="package">Object that is going to be send</param>
        Task SendAsync<T>(string method, T package);
    }
}
