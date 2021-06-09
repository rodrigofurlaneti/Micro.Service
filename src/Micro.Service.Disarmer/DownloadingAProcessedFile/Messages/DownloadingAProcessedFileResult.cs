using System.IO;
using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.DownloadingAProcessedFile.Messages
{
    public class DownloadingAProcessedFileResult
    {
        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("file")]
        public Stream File { get; set; }
    }
}
