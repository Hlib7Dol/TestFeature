using BusWebApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FeatureConsoleApplication.Dto
{
    public record EventDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Starts_At { get; set; }

        [Required]
        public DateTime Expires_At { get; set; }

        [Required]
        public EventType Event_Type { get; set; }
    }
}
