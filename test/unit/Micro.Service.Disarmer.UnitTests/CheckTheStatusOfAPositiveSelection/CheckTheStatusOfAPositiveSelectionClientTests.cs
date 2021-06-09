using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection;
using Micro.Service.Disarmer.CheckTheStatusOfAPositiveSelection.Messages;
using Micro.Service.Base;
using Micro.Service.Base.Config;
using Micro.Service.Base.Exceptions;
using Micro.Service.Base.Messages;
using Micro.Service.Base.UnitTests;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Service.Disarmer.UnitTests.CheckTheStatusOfAPositiveSelection
{
    public class CheckTheStatusOfAPositiveSelectionClientTests
    {
        #region Properties
        private CancellationToken CancellationToken { get; }

        private ILogger<CheckTheStatusOfAPositiveSelectionClient> Logger { get; }
        private CheckTheStatusOfAPositiveSelectionParams Params { get; }
        private Mock<IServiceClient> Client { get; }

        #endregion

        #region Constructor
        public CheckTheStatusOfAPositiveSelectionClientTests()
        {
            Logger = new Mock<ILogger<CheckTheStatusOfAPositiveSelectionClient>>().Object;
            Params = ServiceUnitTestsConfiguration.ReadJson<CheckTheStatusOfAPositiveSelectionParams>("CheckTheStatusOfAPositiveSelectionParams");
            Client = new Mock<IServiceClient>();
            CancellationToken = new CancellationToken();
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Check The Status Of A Positive Selection Success")]
        public async Task ShouldCheckTheStatusOfAPositiveSelectionSuccessfully()
        {
            Client
               .Setup(client => client.CallAsync<CheckTheStatusOfAPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Returns(Task.FromResult(GetCheckTheStatusOfAPositiveSelectionResult()));

            var CheckTheStatusOfAPositiveSelectionClient = new CheckTheStatusOfAPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);
            var result = await CheckTheStatusOfAPositiveSelectionClient.CheckTheStatusOfAPositiveSelectionAsync(Params, CancellationToken);

            result.Result.Should().NotBeEquivalentTo(new CheckTheStatusOfAPositiveSelectionResult());

            Assert.True(result.IsSuccess);
        }

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should Check The Status Of A Positive Selection Throws ServiceException")]
        public void ShouldThwrosServiceExceptionOnCheckTheStatusOfAPositiveSelection()
        {
            Client
               .Setup(client => client.CallAsync<CheckTheStatusOfAPositiveSelectionResult>(It.IsAny<HttpConfig>(),  It.IsAny<CancellationToken>()))
               .Throws(new ServiceException(string.Empty));

            var CheckTheStatusOfAPositiveSelectionClient = new CheckTheStatusOfAPositiveSelectionClient(Options.Create(new ServiceConfig()), Client.Object, Logger);

            Assert.ThrowsAsync<ServiceException>(() => CheckTheStatusOfAPositiveSelectionClient.CheckTheStatusOfAPositiveSelectionAsync(Params, CancellationToken));
        }

        #endregion

        #region Private Methods

        private BaseResult<CheckTheStatusOfAPositiveSelectionResult> GetCheckTheStatusOfAPositiveSelectionResult()
        {
            return new BaseResult<CheckTheStatusOfAPositiveSelectionResult>
            {
                IsSuccess = true,
                Message = "Mock successfully executed",
                ErrorMessage = string.Empty,
                ItemReferenceId = "123222",
                Result = ServiceUnitTestsConfiguration.ReadJson<CheckTheStatusOfAPositiveSelectionResult>("CheckTheStatusOfAPositiveSelectionResult")
            };
        }

        #endregion
    }
}
