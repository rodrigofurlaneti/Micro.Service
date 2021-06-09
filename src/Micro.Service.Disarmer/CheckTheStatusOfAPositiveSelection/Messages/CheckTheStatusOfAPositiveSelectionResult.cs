using System.Text.Json.Serialization;
using Micro.Service.Base.Messages;

namespace Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages
{
    public class CheckTheStatusOfAPositiveSelectionResult : ServiceResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
