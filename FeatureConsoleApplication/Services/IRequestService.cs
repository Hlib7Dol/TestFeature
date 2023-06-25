namespace FeatureConsoleApplication.Services
{
    internal interface IRequestService
    {
        Task<T> GetAsync<T>(string url);
    }
}
