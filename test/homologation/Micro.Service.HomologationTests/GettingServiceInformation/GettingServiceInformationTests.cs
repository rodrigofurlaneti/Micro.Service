using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Micro.Service.Base.HomologationTests;
using Micro.Service.Disarmer.GettingServiceInformation;
using Micro.Service.Disarmer.GettingServiceInformation.Messages;
using Xunit;

namespace Micro.Service.HomologationTests.GettingServiceInformation
{
    public class GettingServiceInformationTests : ServiceHomologationTestsConfiguration
    {
        private ILogger<GettingServiceInformationClient> Logger { get; }

        public GettingServiceInformationTests() : base() 
        {
            Logger = new LoggerFactory().CreateLogger<GettingServiceInformationClient>();
        }

        [Fact(DisplayName = "Should Getting Service Information Successfully", Skip = "true")]
        public async Task ShouldGettingServiceInformation()
        {
            var cancellationToken = new CancellationToken();

            var client = new GettingServiceInformationClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.GettingServiceInformationAsync(cancellationToken);

            result.Should().NotBeEquivalentTo(new GettingServiceInformationResult());
        }

        [Fact(DisplayName = "Client Should Getting Service Information Throw Service Exception", Skip = "true")]
        public async Task ClientShouldThrowServiceException()
        {
            var cancellationToken = new CancellationToken();

            var client = new GettingServiceInformationClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.GettingServiceInformationAsync(cancellationToken);

            Assert.True(result.Message != "");
        }
    }
}
