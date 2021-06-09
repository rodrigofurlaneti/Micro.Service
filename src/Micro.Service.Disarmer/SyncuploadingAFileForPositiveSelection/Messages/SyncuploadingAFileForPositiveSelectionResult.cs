using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages
{
    public class SyncuploadingAFileForPositiveSelectionResult
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
