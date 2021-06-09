using System.Collections.Generic;
using System.Text.Json.Serialization;
using Micro.Service.Base.Messages;

namespace Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages
{
    public class DownloadingAPositiveSelectionReportResult : ServiceResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("relativePath")]
        public string RelativePath { get; set; }

        [JsonPropertyName("originalFileName")]
        public string OriginalFileName { get; set; }

        [JsonPropertyName("sanitizedFileName")]
        public string SanitizedFileName { get; set; }

        [JsonPropertyName("originalSize")]
        public long OriginalSize { get; set; }

        [JsonPropertyName("sanitizedSize")]
        public long SanitizedSize { get; set; }

        [JsonPropertyName("originalPublishStatus")]
        public int OriginalPublishStatus { get; set; }

        [JsonPropertyName("sanitizedPublishStatus")]
        public int SanitizedPublishStatus { get; set; }

        // Precisa de Uma Classe FileType pra isso pra mapear

        [JsonPropertyName("fileType")]
        public object FileType { get; set; }

        [JsonPropertyName("fileType.code")]
        public int FileTypeCode { get; set; }

        [JsonPropertyName("fileType.type")]
        public string FileTypeType { get; set; }

        [JsonPropertyName("fileType.family")]
        public string FileTypeFamily { get; set; }

        [JsonPropertyName("queueTime")]
        public string QueueTime { get; set; }

        [JsonPropertyName("processStartTime")]
        public string ProcessStartTime { get; set; }

        [JsonPropertyName("processEndTime")]
        public string ProcessEndTime { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }

        [JsonPropertyName("children")]
        public object[] Children { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("extendedInfo")]
        public object ExtendedInfo { get; set; }
    }
}
