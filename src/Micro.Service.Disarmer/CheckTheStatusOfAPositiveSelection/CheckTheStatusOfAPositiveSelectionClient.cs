using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;
using System.Net.Http;

namespace Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection
{
    public class CheckTheStatusOfAPositiveSelectionClient : ICheckTheStatusOfAPositiveSelectionClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<CheckTheStatusOfAPositiveSelectionClient> Logger { get; }

        #endregion

        #region Constructor

        public CheckTheStatusOfAPositiveSelectionClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<CheckTheStatusOfAPositiveSelectionClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region ICheckTheStatusOfAPositiveSelectionClient Methods

        public async Task<BaseResult<CheckTheStatusOfAPositiveSelectionResult>> CheckTheStatusOfAPositiveSelectionAsync(CheckTheStatusOfAPositiveSelectionParams parameters, CancellationToken cancellationToken)
        {
            try
            {

                HttpConfig param = new HttpConfig()
                {
                    HttpMethod = HttpMethod.Get,
                    RequestMultipartType = RequestMultipartType.NoMultipart,
                    Endpoint = $"status/{parameters.RequestId}"
                };


                var response = await Client.CallAsync<CheckTheStatusOfAPositiveSelectionResult>(param, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process CheckTheStatusOfAPositiveSelection finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during CheckTheStatusOfAPositiveSelection Operation");
                throw new ServiceException("Error during CheckTheStatusOfAPositiveSelection Operation", ex);
            }
        }

        #endregion
    }
}
