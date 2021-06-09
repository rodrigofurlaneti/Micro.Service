using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.UploadAFileForPositiveSelection;
using Micro.Service.Disarmer.UploadAFileForPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Text;

namespace Micro.Service.Disarmer.UnitTests.UploadAFileForPositiveSelection
{
    public class UploadAFileForPositiveSelectionTests
    {
        #region Properties

        private ILogger<UploadAFileForPositiveSelectionClient> Logger { get; }
        private UploadAFileForPositiveSelectionParams Params { get; }
        private Mock<IServiceClient> Client { get; }
        public CancellationToken CancellationToken { get; }
        public string Text { get; }
        #endregion

        #region Constructor
        public UploadAFileForPositiveSelectionTests()
        {
            Logger = new Mock<ILogger<UploadAFileForPositiveSelectionClient>>().Object;
            Params = ServiceUnitTestsConfiguration.ReadJson<UploadAFileForPositiveSelectionParams>("UploadAFileForPositiveSelectionParams");
            Client = new Mock<IServiceClient>();
            CancellationToken = new CancellationToken();
            Text = "This is some text";
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Upload A File For Positive Selection Success")]
        public async Task ShouldUploadAFileForPositiveSelectionSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<UploadAFileForPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(UploadAFileForPositiveSelectionResult()));
            var UploadAFileForPositiveSelection = new UploadAFileForPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);
            var result = await UploadAFileForPositiveSelection.UploadAFileForPositiveSelectionAsync(Params, CancellationToken);

            result.Result.Should().NotBeEquivalentTo(new UploadAFileForPositiveSelectionResult());

            Assert.True(result.IsSuccess);
        }

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Upload A File For Positive Selection Throws ServiceException")]
        public void ShouldThwrosServiceExceptionOnUploadAFileForPositiveSelection()
        {
            Client
               .Setup(client => client.CallAsync<UploadAFileForPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var UploadAFileForPositiveSelection = new UploadAFileForPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => UploadAFileForPositiveSelection.UploadAFileForPositiveSelectionAsync(Params, CancellationToken));
        }

        #endregion

        #region Private Methods

        private BaseResult<UploadAFileForPositiveSelectionResult> UploadAFileForPositiveSelectionResult()
        {
            return new BaseResult<UploadAFileForPositiveSelectionResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = ServiceUnitTestsConfiguration.ReadJson<UploadAFileForPositiveSelectionResult>("UploadAFileForPositiveSelectionResult")
            };
        }

        #endregion
    }
}
