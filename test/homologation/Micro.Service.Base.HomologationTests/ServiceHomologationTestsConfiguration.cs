using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Base.Config;
using System.Net.Http;
using System.IO;
using System.Text.Json;

namespace Micro.Service.Base.HomologationTests
{
    public class ServiceHomologationTestsConfiguration
    {
        public IOptions<ServiceConfig> ServiceConfig { get; internal set; }
        public ILogger<ServiceClient> ClientLogger { get; internal set; }
        public IServiceClient ServiceClient { get; internal set; }
        public ServiceHomologationTestsConfiguration()
        {
            ClientLogger = new LoggerFactory().CreateLogger<ServiceClient>();
            ServiceConfig = Options.Create(new ServiceConfig
            {
                Url = "https://Service.uat.safrapay.com/disarmer/api/disarmer/v4/",
                Authorization = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBGMDY5OUQ1OUIyMEYyOUE0QTNDQzMyMDU4MEQ0QUNDOTY0NkI1MEQiLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6IlNhZnJhcGF5REVWMDEiLCJncm91cHNpZCI6IlZvdGlyb0ludGVybmFsU2VydmljZXMiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsImp0aSI6IjM0N2YyNzEwLTM4ODYtNGQ4Mi1iZWM3LTY3NDYzZDZhOTQxZiIsIm5iZiI6MTYwOTI4MDAyMywiZXhwIjoxNjQwOTE5NjAwLCJpYXQiOjE2MDkyODAwMjN9.fbZQBE9SPYkpoJEpc0TkobbMEKjvAtiRFxN9mRaB133gC8Yb1shJxTOfa_wxHTqB9UDtl7rzdmnTB962wEWSa9ZSInZSJRxEYAskAY050LBZPzqi3dnblZX8WBNcklVUEbZqZsXAd0BB_RbALfgXKcxq5cHazcRKOD1Xu05gsE34eK-ZxPR7TPuVoqS76YAABudEPmKfvcn-wIGeB8NMgbNCR78W1lnWvs_lMwI-ztckHT36RkYJIg79F1Hy4gmc8PiY6r6Lv_qhDJPOm2t1DoaNZGeFP12mwLPgA6HT5VP3s-SukdNqxhkkbREXwwzlw0Pt4BU2OGe7KaHHyX2uYg",
            });

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            ServiceClient = new ServiceClient(ServiceConfig, new HttpClient(clientHandler), ClientLogger);

        }
        public static T ReadJson<T>(string file)
        {
            using (StreamReader reader = new StreamReader($"../../../{file.Replace("Result", "").Replace("Params", "")}/Inputs/{file}.json"))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json);
            }
        }

    }
}
