using System.Text.Json.Serialization;

namespace Micro.Service.Base.Messages
{
    public class ServiceResult
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("success_code")]
        public int SuccessCode { get; set; }

        [JsonPropertyName("success_message")]
        public string SuccessMessage { get; set; }

        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }
    }
}
