using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Micro.Service.Base.HomologationTests;
using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection;
using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages;
using Xunit;

namespace Micro.Service.HomologationTests.CheckTheStatusOfAPositiveSelection
{
    public class CheckTheStatusOfAPositiveSelectionTests : ServiceHomologationTestsConfiguration
    {
        private ILogger<CheckTheStatusOfAPositiveSelectionClient> Logger { get; }

        public CheckTheStatusOfAPositiveSelectionTests() : base()
        {
            Logger = new LoggerFactory().CreateLogger<CheckTheStatusOfAPositiveSelectionClient>();
        }

        [Fact(DisplayName = "Should Check The Status Of A Positive Selection Successfully", Skip = "true")]
        public async Task ShouldCheckTheStatusOfAPositiveSelection()
        {
            var request = new CheckTheStatusOfAPositiveSelectionParams
            {
                RequestId = "c94f00a4-7a36-4152-977d-dda71cccfb95"
            };

            var cancellationToken = new CancellationToken();

            var client = new CheckTheStatusOfAPositiveSelectionClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.CheckTheStatusOfAPositiveSelectionAsync(request, cancellationToken);

            result.Should().NotBeEquivalentTo(new CheckTheStatusOfAPositiveSelectionResult());
        }

        [Fact(DisplayName = "Client Should Check The Status Of A Positive Selection Throw Service Exception" ,Skip = "true")]
        public async Task ClientShouldThrowServiceException()
        {
            var request = new CheckTheStatusOfAPositiveSelectionParams
            {
                RequestId = "c94f00a4-7a36-4152-977d-dda71cccfb95"
            };

            var cancellationToken = new CancellationToken();

            var client = new CheckTheStatusOfAPositiveSelectionClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.CheckTheStatusOfAPositiveSelectionAsync(request, cancellationToken);

            Assert.True(result.Message != "");
        }
    }
}
