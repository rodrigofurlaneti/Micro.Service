using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection;
using Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Text;

namespace Micro.Service.Disarmer.UnitTests.SyncuploadingAFileForPositiveSelection
{
    public class SyncuploadingAFileForPositiveSelectionTests
    {
        #region Properties

        private ILogger<SyncuploadingAFileForPositiveSelectionClient> Logger { get; }
        private SyncuploadingAFileForPositiveSelectionParams Params { get; }
        private Mock<IServiceClient> Client { get; }
        public CancellationToken CancellationToken { get; }
        public string Text { get; }
        #endregion

        #region Constructor
        public SyncuploadingAFileForPositiveSelectionTests()
        {
            Logger = new Mock<ILogger<SyncuploadingAFileForPositiveSelectionClient>>().Object;
            Params = ServiceUnitTestsConfiguration.ReadJson<SyncuploadingAFileForPositiveSelectionParams>("SyncuploadingAFileForPositiveSelectionParams");
            Client = new Mock<IServiceClient>();
            CancellationToken = new CancellationToken();
            Text = "This is some text";
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Syncuploading A File For Positive Selection Success")]
        public async Task ShouldSyncuploadingAFileForPositiveSelectionSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<SyncuploadingAFileForPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(GetSyncuploadingAFileForPositiveSelectionResult()));
            var SyncuploadingAFileForPositiveSelection = new SyncuploadingAFileForPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);
            var result = await SyncuploadingAFileForPositiveSelection.SyncuploadingAFileForPositiveSelectionAsync(Params, CancellationToken);

            result.Result.Should().NotBeEquivalentTo(new SyncuploadingAFileForPositiveSelectionResult());

            Assert.True(result.IsSuccess);
        }

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Syncuploading A File For Positive Selection Throws ServiceException")]
        public void ShouldThwrosServiceExceptionOnSyncuploadingAFileForPositiveSelection()
        {
            Client
               .Setup(client => client.CallAsync<SyncuploadingAFileForPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var SyncuploadingAFileForPositiveSelection = new SyncuploadingAFileForPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => SyncuploadingAFileForPositiveSelection.SyncuploadingAFileForPositiveSelectionAsync(Params, CancellationToken));
        }

        #endregion

        #region Private Methods

        private BaseResult<SyncuploadingAFileForPositiveSelectionResult> GetSyncuploadingAFileForPositiveSelectionResult()
        {
            return new BaseResult<SyncuploadingAFileForPositiveSelectionResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = ServiceUnitTestsConfiguration.ReadJson<SyncuploadingAFileForPositiveSelectionResult>("SyncuploadingAFileForPositiveSelectionResult")
            };
        }

        #endregion
    }
}
