using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.DownloadingAProcessedFile;
using Micro.Service.Disarmer.DownloadingAProcessedFile.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Service.Disarmer.UnitTests.DownloadingAProcessedFile
{
    public class DownloadingAProcessedFileClientTests
    {
        #region Properties

        private ILogger<DownloadingAProcessedFileClient> Logger;
        private Mock<IServiceClient> Client;
        private DownloadingAProcessedFileParams param = new DownloadingAProcessedFileParams();
        public CancellationToken cancellationToken = new CancellationToken();

        #endregion

        #region Constructor
        public DownloadingAProcessedFileClientTests()
        {
            Logger = new Mock<ILogger<DownloadingAProcessedFileClient>>().Object;
            Client = new Mock<IServiceClient>();
            param = ServiceUnitTestsConfiguration.ReadJson<DownloadingAProcessedFileParams>("DownloadingAProcessedFileParams");
        }

        #endregion

        #region Unit Tests

        [Fact(DisplayName = "Should Downloading A Processed File Success")]
        public async void ShouldDownloadingAProcessedFileSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<DownloadingAProcessedFileResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(GetDownloadingAProcessedFileResult()));
            var DownloadingAProcessedFileClient = new DownloadingAProcessedFileClient(Options.Create(new ServiceConfig()), Client.Object, Logger);
            var result = await DownloadingAProcessedFileClient.DownloadingAProcessedFileAsync(param, cancellationToken);

            result.Result.Should().NotBeEquivalentTo(new DownloadingAProcessedFileResult());

            Assert.True(result.IsSuccess);
        }

        [Fact(DisplayName = "Should Downloading A Processed File Throws Service Exception")]
        public void ShouldThwrosServiceExceptionOnDownloadingAProcessedFile()
        {
            Client
               .Setup(client => client.CallAsync<DownloadingAProcessedFileResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var DownloadingAProcessedFileClient = new DownloadingAProcessedFileClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => DownloadingAProcessedFileClient.DownloadingAProcessedFileAsync(param, cancellationToken));
        }

        #endregion

        #region Private Methods

        private BaseResult<DownloadingAProcessedFileResult> GetDownloadingAProcessedFileResult()
        {
            var result = ServiceUnitTestsConfiguration.ReadJson<DownloadingAProcessedFileResult>("DownloadingAProcessedFileResult");
            return new BaseResult<DownloadingAProcessedFileResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = result
            };
        }

        #endregion
    }
}
