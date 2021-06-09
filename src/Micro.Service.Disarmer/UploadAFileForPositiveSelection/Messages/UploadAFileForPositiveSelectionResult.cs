using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.UploadAFileForPositiveSelection.Messages
{
    public class UploadAFileForPositiveSelectionResult
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }
}
