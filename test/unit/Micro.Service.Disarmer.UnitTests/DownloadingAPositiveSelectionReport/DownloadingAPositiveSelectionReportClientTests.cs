using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport;
using Micro.Service.Disarmer.DownloadingAPositiveSelectionReport.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Service.Disarmer.UnitTests.DownloadingAPositiveSelectionReport
{
    public class DownloadingAPositiveSelectionReportClientTests
    {
        #region Properties
        public CancellationToken CancellationToken { get; }
        private ILogger<DownloadingAPositiveSelectionReportClient> Logger { get; }
        private Mock<IServiceClient> Client { get; }
        private DownloadingAPositiveSelectionReportParams Params { get; }

        #endregion

        #region Constructor
        public DownloadingAPositiveSelectionReportClientTests()
        {
            Logger = new Mock<ILogger<DownloadingAPositiveSelectionReportClient>>().Object;
            Client = new Mock<IServiceClient>();
            Params = ServiceUnitTestsConfiguration.ReadJson<DownloadingAPositiveSelectionReportParams>("DownloadingAPositiveSelectionReportParams");
            CancellationToken = new CancellationToken();
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Downloading A Positive Selection Report Success")]
        public async void ShouldDownloadingAPositiveSelectionReportSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<DownloadingAPositiveSelectionReportResult>(It.IsAny<HttpConfig>(), It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(DownloadingAPositiveSelectionReportResult()));
            var DownloadingAPositiveSelectionReportClient = new DownloadingAPositiveSelectionReportClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            var result = await DownloadingAPositiveSelectionReportClient.DownloadingAPositiveSelectionReportAsync(Params, CancellationToken);

            result.Should().NotBeEquivalentTo(new DownloadingAPositiveSelectionReportResult());
        }

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Downloading A Positive Selection Report Throws ServiceException")]
        public void ShouldThwrosServiceExceptionOnDownloadingAPositiveSelectionReport()
        {
            Client
               .Setup(client => client.CallAsync<DownloadingAPositiveSelectionReportResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var DownloadingAPositiveSelectionReportClient = new DownloadingAPositiveSelectionReportClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => DownloadingAPositiveSelectionReportClient.DownloadingAPositiveSelectionReportAsync(Params, CancellationToken));
        }

        [Fact(DisplayName = "Should Create DownloadAPositiveSelectionReportMessageResult")]
        public void DownloadAPositiveSelectionReportMessageResult()
        {
            var notification = ServiceUnitTestsConfiguration.ReadJson<DownloadingAPositiveSelectionReportResult>("DownloadingAPositiveSelectionReportResult");

            notification.Should().NotBeEquivalentTo(new DownloadingAPositiveSelectionReportResult());
        }

        #endregion

        #region Private Methods

        private BaseResult<DownloadingAPositiveSelectionReportResult> DownloadingAPositiveSelectionReportResult()
        {
            return new BaseResult<DownloadingAPositiveSelectionReportResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = ServiceUnitTestsConfiguration.ReadJson<DownloadingAPositiveSelectionReportResult>("DownloadingAPositiveSelectionReportResult")
            };
        }

        #endregion
    }
}
