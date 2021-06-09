using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Micro.Service.Disarmer.GettingServiceInformation.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.ValueObjects;
using System.Net.Http;

namespace Micro.Service.Disarmer.GettingServiceInformation
{
    public class GettingServiceInformationClient : IGettingServiceInformationClient
    {
        #region Properties

        private ServiceConfig Config { get; }
        private IServiceClient Client { get; }
        private ILogger<GettingServiceInformationClient> Logger { get; }

        #endregion

        #region Constructor

        public GettingServiceInformationClient(IOptions<ServiceConfig> config, IServiceClient client, ILogger<GettingServiceInformationClient> logger)
        {
            Config = config.Value;
            Client = client;
            Logger = logger;
        }

        #endregion

        #region IServiceInformationClient Methods
        public async Task<BaseResult<GettingServiceInformationResult>> GettingServiceInformationAsync(CancellationToken cancellationToken)
        {
            try
            {
                var message = new HttpConfig
                {
                    HttpMethod = HttpMethod.Get,
                    RequestMultipartType = RequestMultipartType.NoMultipart,
                    Endpoint = "about",                   
                };

                var response = await Client.CallAsync<GettingServiceInformationResult>(message, cancellationToken);

                Logger.LogDebug($"[Proxy:Service] Process ServiceInformation finished - success: '{response.IsSuccess}'");

                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "[Proxy:Service] Error during ServiceInformation Operation");
                throw new ServiceException("Error during ServiceInformation Operation", ex);
            }
        }

        #endregion
    }
}
