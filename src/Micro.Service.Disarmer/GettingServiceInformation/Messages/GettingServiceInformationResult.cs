using System.Text.Json.Serialization;
namespace Micro.Service.Disarmer.GettingServiceInformation.Messages
{
    public class GettingServiceInformationResult
    {
        [JsonPropertyName("ApiVersion")]
        public string ApiVersion { get; set; }
    }
}
