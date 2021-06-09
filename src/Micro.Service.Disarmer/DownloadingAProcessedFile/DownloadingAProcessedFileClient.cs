using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.DownloadingAProcessedFile.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;
using System.Net.Http;

namespace Micro.Service.Disarmer.DownloadingAProcessedFile
{
    public class DownloadingAProcessedFileClient : IDownloadingAProcessedFileClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<DownloadingAProcessedFileClient> Logger { get; }

        #endregion

        #region Constructor

        public DownloadingAProcessedFileClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<DownloadingAProcessedFileClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region IDownloadingAProcessedFileClient Methods

        public async Task<BaseResult<DownloadingAProcessedFileResult>> DownloadingAProcessedFileAsync(DownloadingAProcessedFileParams parameters, CancellationToken cancellationToken)
        {
            try
            {
                var message = new HttpConfig
                {
                    HttpMethod = HttpMethod.Get,
                    RequestMultipartType = RequestMultipartType.Download,
                    Endpoint = $"download/{parameters.RequestId}"
                };

                var response = await Client.CallAsync<DownloadingAProcessedFileResult>(message, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process DownloadingAProcessedFile finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during DownloadingAProcessedFile Operation");
                throw new ServiceException("Error during DownloadingAProcessedFile Operation", ex);
            }
        }

        #endregion
    }
}
