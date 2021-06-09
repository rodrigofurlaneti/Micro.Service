using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages
{
    public class Event
    {
        [JsonPropertyName("events.date")]
        public string Date { get; set; }

        [JsonPropertyName("event.id")]
        public int EventId { get; set; }

        [JsonPropertyName("event.name")]
        public string EventName { get; set; }

        [JsonPropertyName("event.message")]
        public string EventMessage { get; set; }

        [JsonPropertyName("event.severity")]
        public int EventSeverity { get; set; }

        [JsonPropertyName("event.category")]
        public long EventCategory { get; set; }

        [JsonPropertyName("event.subCategory")]
        public long EventSubCategory { get; set; }
    }
}
