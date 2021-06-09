using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;


namespace Micro.Service.Base
{
    public class ServiceClient : IServiceClient
    {

        #region Properties

        private ServiceConfig Config { get; }
        private HttpClient Client { get; }
        private ILogger<ServiceClient> Logger { get; }


        #endregion

        #region Constructor

        public ServiceClient(IOptions<ServiceConfig> config, HttpClient client, ILogger<ServiceClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Config.Authorization);
        }

        #endregion

        #region IServiceClient implementation

        public async Task<BaseResult<TResponse>> CallAsync<TResponse>(HttpConfig httpConfig, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = null;
            var baseResponse = new BaseResult<TResponse>();
            try
            {
                
                string path = Path.Combine(Config.Url, httpConfig.Endpoint);
                string body = string.Empty;

                if (Config == null)
                {
                    Logger.LogError("[Proxy:Service] ServiceConfig was not found in the configuration");
                    throw new ServiceException("[Proxy:Service] ServiceConfig  was not found in the configuration");
                }

                Logger.LogInformation($"[Proxy:Service] Request body for endpoint: '{path}' created: {httpConfig.Body}");

                if (httpConfig.Body != null)
                {
                    body = JsonConvert.SerializeObject(httpConfig.Body);
                }

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(path),
                    Method = httpConfig.HttpMethod
                };

                if (httpConfig.RequestMultipartType == RequestMultipartType.NoMultipart)
                {
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
                    httpResponseMessage = await Client.SendAsync(requestMessage);
                }
                else if (httpConfig.RequestMultipartType == RequestMultipartType.Download)
                {
                    httpResponseMessage = await DownloadFile(httpResponseMessage, requestMessage);
                }
                else
                {
                    httpResponseMessage = await UploadFile(httpResponseMessage, requestMessage, httpConfig);
                }

                ValidateResponse(httpResponseMessage, httpConfig.Endpoint, baseResponse);
                baseResponse.IsSuccess = httpResponseMessage.IsSuccessStatusCode;
                return baseResponse;
            }
            catch (Exception ex)
            {
                baseResponse.IsSuccess = false;
                Logger.LogError($"[Proxy:Service] Error during CallAsync \n {ex}");
                throw new ServiceException("[Proxy:Service]: Error on service CallAsync", ex);
            }
        }

        #endregion

        #region Methods

        private void ValidateResponse<TResponse>(HttpResponseMessage httpResponseMessage, string endpoint, BaseResult<TResponse> baseResponse)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                baseResponse.IsSuccess = true;
                if (httpResponseMessage.Content.Headers.ContentType.MediaType == "application/json")
                {
                    baseResponse.Message = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    baseResponse.Result = JsonConvert.DeserializeObject<TResponse>(httpResponseMessage.Content.ReadAsStringAsync().Result);
                }
                if (httpResponseMessage.Content.Headers.ContentType.MediaType == "text/plain")
                {
                    baseResponse.Message = httpResponseMessage.Content.ReadAsStringAsync().Result;
                }
                if (httpResponseMessage.Content.Headers.ContentType.MediaType == "application/octet-stream")
                {
                    Stream downloadedFile = httpResponseMessage.Content.ReadAsStreamAsync().Result;
                    baseResponse.File = downloadedFile;
                    baseResponse.Message = httpResponseMessage.Content.ReadAsStringAsync().Result;
                }
                Logger.LogDebug($"[Proxy:Service] Request completed with success");
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                baseResponse.IsSuccess = false;
                Logger.LogError($"[Proxy:Service] Bad request while trying to call {endpoint}");
                baseResponse.Message = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                baseResponse.IsSuccess = false;
                Logger.LogError($"[Proxy:Service] Returned statusCode: '{httpResponseMessage.StatusCode}' while trying to call {endpoint}");
                baseResponse.Message = httpResponseMessage.Content.ReadAsStringAsync().Result;
                baseResponse.Result = JsonConvert.DeserializeObject<TResponse>(baseResponse.Message);
                throw new ServiceException(httpResponseMessage.ReasonPhrase, baseResponse.Message, (int)httpResponseMessage.StatusCode);
            }
        }

        private async Task<HttpResponseMessage> UploadFile(HttpResponseMessage httpResponseMessage, HttpRequestMessage requestMessage, HttpConfig httpConfig)
        {
            requestMessage.Content = new MultipartFormDataContent()
            {
                {
                    new StreamContent(new MemoryStream(httpConfig.BinaryFile)),"file", $"{httpConfig.FileName}.{httpConfig.Format}"
                }
            };
            httpResponseMessage = await Client.SendAsync(requestMessage);
            Logger.LogInformation($"[Proxy:Service] Response body for endpoint: '{requestMessage.RequestUri}' created: {httpResponseMessage.StatusCode}");
            return httpResponseMessage;
        }

        private async Task<HttpResponseMessage> DownloadFile(HttpResponseMessage httpResponseMessage, HttpRequestMessage requestMessage)
        {
            httpResponseMessage = await Client.SendAsync(requestMessage);
            Logger.LogInformation($"[Proxy:Service] Response body for endpoint: '{requestMessage.RequestUri}' created: {httpResponseMessage.StatusCode}");
            return httpResponseMessage;
        }

        #endregion
    }
}
