using BusWebApi.Enum;

namespace BusWebApi.Models
{
    /// <summary>
    /// Base storage model
    /// </summary>
    public record DbModel
    {
        public long Id { get; set; }

        public SyncState SendState { get; set; }
    }
}
