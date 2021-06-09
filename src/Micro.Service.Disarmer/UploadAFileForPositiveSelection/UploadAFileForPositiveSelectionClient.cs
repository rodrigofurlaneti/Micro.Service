using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.UploadAFileForPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;
using System.Net.Http;

namespace Micro.Service.Disarmer.UploadAFileForPositiveSelection
{
    public class UploadAFileForPositiveSelectionClient : IUploadAFileForPositiveSelectionClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<UploadAFileForPositiveSelectionClient> Logger { get; }

        #endregion

        #region Constructor

        public UploadAFileForPositiveSelectionClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<UploadAFileForPositiveSelectionClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region IUploadAFileForPositiveSelectionClient Methods

        public async Task<BaseResult<UploadAFileForPositiveSelectionResult>> UploadAFileForPositiveSelectionAsync(UploadAFileForPositiveSelectionParams parameters, CancellationToken cancellationToken)
        {
            try
            {
                var config = new HttpConfig
                {
                    HttpMethod = HttpMethod.Post,
                    RequestMultipartType = RequestMultipartType.Upload,
                    Endpoint = $"upload",
                    BinaryFile = parameters.BinaryFile,
                    FileName = parameters.FileName,
                    Format = parameters.Format
                };

                var response = await Client.CallAsync<UploadAFileForPositiveSelectionResult>(config, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process UploadAFileForPositiveSelection finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during UploadAFileForPositiveSelection Operation");
                throw new ServiceException("Error during UploadAFileForPositiveSelection Operation", ex);
            }
        }

        #endregion
    }
}
