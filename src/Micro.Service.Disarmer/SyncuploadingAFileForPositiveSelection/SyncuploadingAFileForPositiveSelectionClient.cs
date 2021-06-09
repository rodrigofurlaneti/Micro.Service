using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;
using System.Net.Http;

namespace Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection
{
    public class SyncuploadingAFileForPositiveSelectionClient : ISyncuploadingAFileForPositiveSelectionClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<SyncuploadingAFileForPositiveSelectionClient> Logger { get; }

        #endregion

        #region Constructor

        public SyncuploadingAFileForPositiveSelectionClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<SyncuploadingAFileForPositiveSelectionClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region ISyncuploadingAFileForPositiveSelectionClient Methods

        public async Task<BaseResult<SyncuploadingAFileForPositiveSelectionResult>> SyncuploadingAFileForPositiveSelectionAsync(SyncuploadingAFileForPositiveSelectionParams parameters, CancellationToken cancellationToken)
        {
            try
            {

                var message = new HttpConfig
                {
                    HttpMethod = HttpMethod.Post,
                    RequestMultipartType = RequestMultipartType.Upload,
                    Endpoint = $"upload-sync",
                    BinaryFile = parameters.BinaryFile,
                    FileName = parameters.FileName,
                    Format = parameters.Format
                };

                var response = await Client.CallAsync<SyncuploadingAFileForPositiveSelectionResult>(message, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process SyncuploadingAFileForPositiveSelection finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during SyncuploadingAFileForPositiveSelection Operation");
                throw new ServiceException("Error during SyncuploadingAFileForPositiveSelection Operation", ex);
            }
        }

        #endregion
    }
}
