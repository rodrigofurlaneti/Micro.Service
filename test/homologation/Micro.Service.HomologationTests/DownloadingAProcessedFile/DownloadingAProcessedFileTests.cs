using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.HomologationTests;
using Micro.Service.Disarmer.DownloadingAProcessedFile;
using Micro.Service.Disarmer.DownloadingAProcessedFile.Messages;
using Xunit;

namespace Micro.Service.HomologationTests.DownloadingAProcessedFile
{
    public class DownloadingAProcessedFileTests : ServiceHomologationTestsConfiguration
    {
        private ILogger<DownloadingAProcessedFileClient> Logger { get; }

        public DownloadingAProcessedFileTests() : base()
        {
            Logger = new LoggerFactory().CreateLogger<DownloadingAProcessedFileClient>();
        }

        [Fact(DisplayName = "Should Downloading A Processed File", Skip = "true")]
        public async Task ShouldDownloadingAProcessedFile()
        {
            var request = new DownloadingAProcessedFileParams
            {
                RequestId = "e842bf96-d517-43ec-9197-7d321c48b2bf"
            };

            var cancellationToken = new CancellationToken();

            var client = new DownloadingAProcessedFileClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.DownloadingAProcessedFileAsync(request, cancellationToken);

            result.Should().NotBeEquivalentTo(new DownloadingAProcessedFileResult());
        }

        [Fact(DisplayName = "Client Should Downloading A Processed File Throw Service Exception", Skip = "true")]
        public void ClientShouldThrowServiceExceptionDownloadingAProcessedFile()
        {
            var request = new DownloadingAProcessedFileParams
            {
                RequestId = "c842bf96-d517-43ec-9197-7d321c48b2bf"
            };

            var cancellationToken = new CancellationToken();

            var client = new DownloadingAProcessedFileClient(ServiceConfig, ServiceClient, Logger);

            Assert.ThrowsAsync<ServiceException>(() => client.DownloadingAProcessedFileAsync(request, cancellationToken));
        }
    }
}
