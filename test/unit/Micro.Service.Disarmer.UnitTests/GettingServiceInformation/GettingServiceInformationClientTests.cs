using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.GettingServiceInformation;
using Micro.Service.Disarmer.GettingServiceInformation.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace Micro.Service.Disarmer.UnitTests.GettingServiceInformation
{
    public class GettingServiceInformationClientTests
    {
        #region Properties

        private ILogger<GettingServiceInformationClient> Logger { get; }
        private Mock<IServiceClient> Client { get; }
        public CancellationToken CancellationToken { get; }

        #endregion

        #region Constructor
        public GettingServiceInformationClientTests()
        {
            Logger = new Mock<ILogger<GettingServiceInformationClient>>().Object;
            Client = new Mock<IServiceClient>();
            CancellationToken = new CancellationToken();
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Getting Service Information Success")]
        public async Task ShouldGettingServiceInformationSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<GettingServiceInformationResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(GettingServiceInformationResult()));
            var serviceInformationClient = new GettingServiceInformationClient(Options.Create(new ServiceConfig()), Client.Object, Logger);
            var result = await serviceInformationClient.GettingServiceInformationAsync(CancellationToken);

            Assert.True(result.IsSuccess);
        }

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Getting Service Information Throws Service Exception")]
        public void ShouldThwrosServiceExceptionOnGettingServiceInformation()
        {
            Client
               .Setup(client => client.CallAsync<GettingServiceInformationResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var serviceInformationClient = new GettingServiceInformationClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => serviceInformationClient.GettingServiceInformationAsync(CancellationToken));
        }

        #endregion

        #region Private Methods

        private BaseResult<GettingServiceInformationResult> GettingServiceInformationResult()
        {
            return new BaseResult<GettingServiceInformationResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = ServiceUnitTestsConfiguration.ReadJson<GettingServiceInformationResult>("GettingServiceInformationResult")
            };
        }

        #endregion
    }
}
