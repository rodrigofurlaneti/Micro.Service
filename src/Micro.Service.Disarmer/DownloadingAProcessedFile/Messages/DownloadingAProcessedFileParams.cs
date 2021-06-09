using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.DownloadingAProcessedFile.Messages
{
    public class DownloadingAProcessedFileParams
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }
}