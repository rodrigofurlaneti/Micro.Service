using System;
using Micro.Service.Base.Exceptions;
using Xunit;

namespace Micro.Service.Disarmer.UnitTests.Others
{
    public class ServiceExceptionTests
    {
        [Fact(DisplayName = "Should Create Service Exception With Message")]
        public void ShouldCreateServiceExceptionWithMessage()
        {
            var exception = new ServiceException("Test");

            Assert.True(exception.Message != null);
        }

        [Fact(DisplayName = "Should Create Service Exception With Message and Details")]
        public void ShouldCreateServiceExceptionWithMessageAndDetails()
        {
            var exception = new ServiceException("Test","details");

            Assert.True(exception.Message != null && exception.Details != null);
        }

        [Fact(DisplayName = "Should Create Service Exception With Message and Inner Exception")]
        public void ShouldCreateServiceExceptionWithMessageAndInnerException()
        {
            var exception = new ServiceException("Test", new Exception("Inner Exception"));

            Assert.True(exception.Message != null && exception.InnerException.Message != null);
        }

        [Fact(DisplayName = "Should Create Service Exception With Message, Details and Status Code")]
        public void ShouldCreateServiceExceptionWithMessageAndDetailsAndStatusCode()
        {
            var exception = new ServiceException("Test","details",400);

            Assert.True(exception.Message != null && exception.Details != null && exception.StatusCode != null);
        }
    }
}
