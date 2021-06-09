using System.Text.Json.Serialization;

namespace Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages
{
    public class CheckTheStatusOfAPositiveSelectionParams
    {
        [JsonPropertyName("requestId")]
        public string RequestId { get; set; }
    }
}
