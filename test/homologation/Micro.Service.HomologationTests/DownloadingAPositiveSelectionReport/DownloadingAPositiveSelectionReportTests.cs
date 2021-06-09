using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Micro.Service.Base.HomologationTests;
using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport;
using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages;
using Xunit;

namespace Micro.Service.HomologationTests.DownloadingAPositiveSelectionReport
{
    public class DownloadingAPositiveSelectionReportTests : ServiceHomologationTestsConfiguration
    {
        private ILogger<DownloadingAPositiveSelectionReportClient> Logger { get; }

        public DownloadingAPositiveSelectionReportTests() : base()
        {
            Logger = new LoggerFactory().CreateLogger<DownloadingAPositiveSelectionReportClient>();
        }

        [Fact(DisplayName = "Should Downloading A Positive Selection Report", Skip = "true")]
        public async Task ShouldDownloadingAPositiveSelectionReport()
        {
            var request = new DownloadingAPositiveSelectionReportParams
            {
                RequestId = "e842bf96-d517-43ec-9197-7d321c48b2bf"
            };

            var cancellationToken = new CancellationToken();

            var client = new DownloadingAPositiveSelectionReportClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.DownloadingAPositiveSelectionReportAsync(request, cancellationToken);

            result.Should().NotBeEquivalentTo(new DownloadingAPositiveSelectionReportResult());
        }

        [Fact(DisplayName = "Client Should Downloading A Positive Selection Report Throw Service Exception", Skip = "true")]
        public async Task ClientShouldThrowServiceExceptionDownloadingAPositiveSelectionReport()
        {
            var request = new DownloadingAPositiveSelectionReportParams
            {
                RequestId = "e842bf96-d517-43ec-9197-7d321c48b2bf"
            };

            var cancellationToken = new CancellationToken();

            var client = new DownloadingAPositiveSelectionReportClient(ServiceConfig, ServiceClient, Logger);

            var result = await client.DownloadingAPositiveSelectionReportAsync(request, cancellationToken);

            result.Should().NotBeEquivalentTo(new DownloadingAPositiveSelectionReportResult());
        }
    }
}
