using Microsoft.AspNetCore.SignalR;

namespace BusWebApi.Services
{
    /// <summary>
    /// SignalR message sender service
    /// </summary>
    public class SignalRMessageSenderService : ISignalRMessageSenderService
    {
        private readonly IHubContext<SignalRHub> _signarlRHub;

        /// <summary>
        /// Constructor for <see cref="SignalRMessageSenderService"/>
        /// </summary>
        /// <param name="signalRHub">SignalR hub</param>
        public SignalRMessageSenderService(IHubContext<SignalRHub> signalRHub)
        { 
            this._signarlRHub = signalRHub;
        }

        /// <summary>
        /// Sends message to Azure SignalR service
        /// </summary>
        /// <typeparam name="T">Any type of parameter</typeparam>
        /// <param name="method">Topic method name</param>
        /// <param name="obj">Object that is going to be send</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="obj"/> is null</exception>
        public async Task SendAsync<T>(string method, T obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            await this._signarlRHub.Clients.All.SendAsync(method, obj);
        }
    }
}
