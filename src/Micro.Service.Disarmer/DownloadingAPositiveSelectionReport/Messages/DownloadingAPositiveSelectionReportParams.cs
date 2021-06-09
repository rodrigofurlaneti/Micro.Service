using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages
{
    public class DownloadingAPositiveSelectionReportParams
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }
}
