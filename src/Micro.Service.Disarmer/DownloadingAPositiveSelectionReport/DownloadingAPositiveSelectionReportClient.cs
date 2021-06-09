using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;

namespace Micro.Service.Disarmer.DownloadingAPositiveSelectionReport
{
    public class DownloadingAPositiveSelectionReportClient : IDownloadingAPositiveSelectionReportClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<DownloadingAPositiveSelectionReportClient> Logger { get; }

        #endregion

        #region Constructor

        public DownloadingAPositiveSelectionReportClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<DownloadingAPositiveSelectionReportClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region IDownloadingAPositiveSelectionReportClient Methods

        public async Task<BaseResult<DownloadingAPositiveSelectionReportResult>> DownloadingAPositiveSelectionReportAsync(DownloadingAPositiveSelectionReportParams parameters, CancellationToken cancellationToken)
        {
            try
            {
                var message = new HttpConfig
                {
                    HttpMethod = HttpMethod.Get,
                    RequestMultipartType = RequestMultipartType.NoMultipart,
                    Endpoint = $"report/{parameters.RequestId}"
                };

                var response = await Client.CallAsync<DownloadingAPositiveSelectionReportResult>(message, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process DownloadingAPositiveSelectionReport finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during DownloadingAPositiveSelectionReport Operation");
                throw new ServiceException("Error during DownloadingAPositiveSelectionReport Operation", ex);
            }
        }

        #endregion
    }
}
