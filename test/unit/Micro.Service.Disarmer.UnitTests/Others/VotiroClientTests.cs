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
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using Moq.Protected;
using Micro.Service.Base.ValueObjects;
using System.Text;
using Micro.Service.Disarmer.SyncuploadingAFileForPositiveSelection.Messages;
using System.IO;
using Micro.Service.Disarmer.DownloadingAProcessedFile.Messages;

namespace Micro.Service.Disarmer.UnitTests.CheckTheStatusOfAPositiveSelection
{
    public class ServiceClientTests
    {
        #region Properties
        private CancellationToken CancellationToken { get; }
        private ILogger<CheckTheStatusOfAPositiveSelectionClient> Logger { get; }
        private CheckTheStatusOfAPositiveSelectionParams Params { get; }
        private Mock<IServiceClient> Client { get; }
        private Mock<ILogger<ServiceClient>> ServiceClientLogger { get; set; }
        private IOptions<ServiceConfig> options { get; set; }

        #endregion

        #region Constructor
        public ServiceClientTests()
        {
            options = Options.Create(new ServiceConfig()
            {
                // Não baterá em HML, baterá no Mock
                Url = "https://Service.uat.safrapay.com/disarmer/api/disarmer/v4/",
                Authorization = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjBGMDY5OUQ1OUIyMEYyOUE0QTNDQzMyMDU4MEQ0QUNDOTY0NkI1MEQiLCJ0eXAiOiJKV1QifQ.eyJ1bmlxdWVfbmFtZSI6IlNhZnJhcGF5REVWMDEiLCJncm91cHNpZCI6IlZvdGlyb0ludGVybmFsU2VydmljZXMiLCJyb2xlIjoiQWRtaW5pc3RyYXRvciIsImp0aSI6IjM0N2YyNzEwLTM4ODYtNGQ4Mi1iZWM3LTY3NDYzZDZhOTQxZiIsIm5iZiI6MTYwOTI4MDAyMywiZXhwIjoxNjQwOTE5NjAwLCJpYXQiOjE2MDkyODAwMjN9.fbZQBE9SPYkpoJEpc0TkobbMEKjvAtiRFxN9mRaB133gC8Yb1shJxTOfa_wxHTqB9UDtl7rzdmnTB962wEWSa9ZSInZSJRxEYAskAY050LBZPzqi3dnblZX8WBNcklVUEbZqZsXAd0BB_RbALfgXKcxq5cHazcRKOD1Xu05gsE34eK-ZxPR7TPuVoqS76YAABudEPmKfvcn-wIGeB8NMgbNCR78W1lnWvs_lMwI-ztckHT36RkYJIg79F1Hy4gmc8PiY6r6Lv_qhDJPOm2t1DoaNZGeFP12mwLPgA6HT5VP3s-SukdNqxhkkbREXwwzlw0Pt4BU2OGe7KaHHyX2uYg",

            });
            Logger = new Mock<ILogger<CheckTheStatusOfAPositiveSelectionClient>>().Object;
            ServiceClientLogger = new Mock<ILogger<ServiceClient>>();
            Params = ServiceUnitTestsConfiguration.ReadJson<CheckTheStatusOfAPositiveSelectionParams>("CheckTheStatusOfAPositiveSelectionParams");
            Client = new Mock<IServiceClient>();
            CancellationToken = new CancellationToken();
        }

        private HttpClient CreateHttpClient<TContent>(TContent content, string mediaType)
        {
            var response = new HttpResponseMessage();
            var stream = content.As<MemoryStream>();
            var handlerMock = new Mock<HttpMessageHandler>();
            if (mediaType == "application/octet-stream")
            {
                response = new HttpResponseMessage
                {
                    ReasonPhrase = "ReasonPhrase",
                    Version = new Version(),
                    RequestMessage = new HttpRequestMessage(),
                    StatusCode = HttpStatusCode.OK,
                    Content = new StreamContent(stream,stream.ToArray().Length),
                    
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentLength = stream.Length;
            }
            else
            {
                string formattedContent;
                if (typeof(TContent) != typeof(string))
                {
                    formattedContent = JsonConvert.SerializeObject(content);
                }
                else
                {
                    formattedContent = (string)Convert.ChangeType(content, typeof(TContent));
                }
                response = new HttpResponseMessage
                {
                    ReasonPhrase = "ReasonPhrase",
                    Version = new Version(),
                    RequestMessage = new HttpRequestMessage(),
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(formattedContent, Encoding.UTF8, mediaType)
                };
            }

            
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            return new HttpClient(handlerMock.Object);
        }

        private BaseResult<TResponse> CreateExpectedResultFileContent<TResponse>(TResponse response)
        {
            return new BaseResult<TResponse>
            {
                ErrorMessage = null,
                File = response.As<MemoryStream>(),
                IsSuccess = true,
                ItemReferenceId = null,
                Message = ""
            };
        }

        

        private BaseResult<TResponse> CreateExpectedResultTextPlain<TResponse>(TResponse response)
        {
            return new BaseResult<TResponse>
            {
                ErrorMessage = null,
                File = null,
                IsSuccess = true,
                ItemReferenceId = null,
                Message = response.ToString()
            };
        }

        private BaseResult<TResponse> CreateExpectedResultApplicationJson<TResponse>(TResponse response)
        {
            return new BaseResult<TResponse>
            {
                ErrorMessage = null,
                File = null,
                IsSuccess = true,
                ItemReferenceId = null,
                Message = response.ToString(),
                Result = response
            };
        }

        #endregion

        #region Unit Tests

        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should CallAsync No Multipart")]
        public async Task ShouldCallAsync_NoMultipart()
        {
            HttpConfig param = new HttpConfig()
            {
                HttpMethod = HttpMethod.Get,
                RequestMultipartType = RequestMultipartType.NoMultipart,
                Endpoint = $"status/e842bf96-d517-43ec-9197-7d321c48b2bf"
            };
            var httpClient = CreateHttpClient("NotExist", "text/plain");
            var ServiceClient = new ServiceClient(options, httpClient, ServiceClientLogger.Object);
            var result = await ServiceClient.CallAsync<CheckTheStatusOfAPositiveSelectionResult>(param, CancellationToken);
            var expectedResult = CreateExpectedResultTextPlain("NotExist");
            result.Should().BeEquivalentTo(expectedResult);
        }


        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should CallAsync_Upload")]
        public async Task ShouldCallAsync_Upload()
        {
            HttpConfig param = new HttpConfig()
            {
                HttpMethod = HttpMethod.Post,
                RequestMultipartType = RequestMultipartType.Upload,
                Endpoint = $"upload-sync",
                BinaryFile = Encoding.UTF8.GetBytes("JVBERi0xLjMNCiXi48/TDQoNCjEgMCBvYmoNCjw8DQovVHlwZSAvQ2F0YWxvZw0KL091dGxpbmVzIDIgMCBSDQovUGFnZXMgMyAwIFINCj4+DQplbmRvYmoNCg0KMiAwIG9iag0KPDwNCi9UeXBlIC9PdXRsaW5lcw0KL0NvdW50IDANCj4+DQplbmRvYmoNCg0KMyAwIG9iag0KPDwNCi9UeXBlIC9QYWdlcw0KL0NvdW50IDINCi9LaWRzIFsgNCAwIFIgNiAwIFIgXSANCj4+DQplbmRvYmoNCg0KNCAwIG9iag0KPDwNCi9UeXBlIC9QYWdlDQovUGFyZW50IDMgMCBSDQovUmVzb3VyY2VzIDw8DQovRm9udCA8PA0KL0YxIDkgMCBSIA0KPj4NCi9Qcm9jU2V0IDggMCBSDQo+Pg0KL01lZGlhQm94IFswIDAgNjEyLjAwMDAgNzkyLjAwMDBdDQovQ29udGVudHMgNSAwIFINCj4+DQplbmRvYmoNCg0KNSAwIG9iag0KPDwgL0xlbmd0aCAxMDc0ID4+DQpzdHJlYW0NCjIgSg0KQlQNCjAgMCAwIHJnDQovRjEgMDAyNyBUZg0KNTcuMzc1MCA3MjIuMjgwMCBUZA0KKCBBIFNpbXBsZSBQREYgRmlsZSApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDY4OC42MDgwIFRkDQooIFRoaXMgaXMgYSBzbWFsbCBkZW1vbnN0cmF0aW9uIC5wZGYgZmlsZSAtICkgVGoNCkVUDQpCVA0KL0YxIDAwMTAgVGYNCjY5LjI1MDAgNjY0LjcwNDAgVGQNCigganVzdCBmb3IgdXNlIGluIHRoZSBWaXJ0dWFsIE1lY2hhbmljcyB0dXRvcmlhbHMuIE1vcmUgdGV4dC4gQW5kIG1vcmUgKSBUag0KRVQNCkJUDQovRjEgMDAxMCBUZg0KNjkuMjUwMCA2NTIuNzUyMCBUZA0KKCB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDYyOC44NDgwIFRkDQooIEFuZCBtb3JlIHRleHQuIEFuZCBtb3JlIHRleHQuIEFuZCBtb3JlIHRleHQuIEFuZCBtb3JlIHRleHQuIEFuZCBtb3JlICkgVGoNCkVUDQpCVA0KL0YxIDAwMTAgVGYNCjY5LjI1MDAgNjE2Ljg5NjAgVGQNCiggdGV4dC4gQW5kIG1vcmUgdGV4dC4gQm9yaW5nLCB6enp6ei4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kICkgVGoNCkVUDQpCVA0KL0YxIDAwMTAgVGYNCjY5LjI1MDAgNjA0Ljk0NDAgVGQNCiggbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDU5Mi45OTIwIFRkDQooIEFuZCBtb3JlIHRleHQuIEFuZCBtb3JlIHRleHQuICkgVGoNCkVUDQpCVA0KL0YxIDAwMTAgVGYNCjY5LjI1MDAgNTY5LjA4ODAgVGQNCiggQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgKSBUag0KRVQNCkJUDQovRjEgMDAxMCBUZg0KNjkuMjUwMCA1NTcuMTM2MCBUZA0KKCB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBFdmVuIG1vcmUuIENvbnRpbnVlZCBvbiBwYWdlIDIgLi4uKSBUag0KRVQNCmVuZHN0cmVhbQ0KZW5kb2JqDQoNCjYgMCBvYmoNCjw8DQovVHlwZSAvUGFnZQ0KL1BhcmVudCAzIDAgUg0KL1Jlc291cmNlcyA8PA0KL0ZvbnQgPDwNCi9GMSA5IDAgUiANCj4+DQovUHJvY1NldCA4IDAgUg0KPj4NCi9NZWRpYUJveCBbMCAwIDYxMi4wMDAwIDc5Mi4wMDAwXQ0KL0NvbnRlbnRzIDcgMCBSDQo+Pg0KZW5kb2JqDQoNCjcgMCBvYmoNCjw8IC9MZW5ndGggNjc2ID4+DQpzdHJlYW0NCjIgSg0KQlQNCjAgMCAwIHJnDQovRjEgMDAyNyBUZg0KNTcuMzc1MCA3MjIuMjgwMCBUZA0KKCBTaW1wbGUgUERGIEZpbGUgMiApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDY4OC42MDgwIFRkDQooIC4uLmNvbnRpbnVlZCBmcm9tIHBhZ2UgMS4gWWV0IG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gKSBUag0KRVQNCkJUDQovRjEgMDAxMCBUZg0KNjkuMjUwMCA2NzYuNjU2MCBUZA0KKCBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSB0ZXh0LiBBbmQgbW9yZSApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDY2NC43MDQwIFRkDQooIHRleHQuIE9oLCBob3cgYm9yaW5nIHR5cGluZyB0aGlzIHN0dWZmLiBCdXQgbm90IGFzIGJvcmluZyBhcyB3YXRjaGluZyApIFRqDQpFVA0KQlQNCi9GMSAwMDEwIFRmDQo2OS4yNTAwIDY1Mi43NTIwIFRkDQooIHBhaW50IGRyeS4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gQW5kIG1vcmUgdGV4dC4gKSBUag0KRVQNCkJUDQovRjEgMDAxMCBUZg0KNjkuMjUwMCA2NDAuODAwMCBUZA0KKCBCb3JpbmcuICBNb3JlLCBhIGxpdHRsZSBtb3JlIHRleHQuIFRoZSBlbmQsIGFuZCBqdXN0IGFzIHdlbGwuICkgVGoNCkVUDQplbmRzdHJlYW0NCmVuZG9iag0KDQo4IDAgb2JqDQpbL1BERiAvVGV4dF0NCmVuZG9iag0KDQo5IDAgb2JqDQo8PA0KL1R5cGUgL0ZvbnQNCi9TdWJ0eXBlIC9UeXBlMQ0KL05hbWUgL0YxDQovQmFzZUZvbnQgL0hlbHZldGljYQ0KL0VuY29kaW5nIC9XaW5BbnNpRW5jb2RpbmcNCj4+DQplbmRvYmoNCg0KMTAgMCBvYmoNCjw8DQovQ3JlYXRvciAoUmF2ZSBcKGh0dHA6Ly93d3cubmV2cm9uYS5jb20vcmF2ZVwpKQ0KL1Byb2R1Y2VyIChOZXZyb25hIERlc2lnbnMpDQovQ3JlYXRpb25EYXRlIChEOjIwMDYwMzAxMDcyODI2KQ0KPj4NCmVuZG9iag0KDQp4cmVmDQowIDExDQowMDAwMDAwMDAwIDY1NTM1IGYNCjAwMDAwMDAwMTkgMDAwMDAgbg0KMDAwMDAwMDA5MyAwMDAwMCBuDQowMDAwMDAwMTQ3IDAwMDAwIG4NCjAwMDAwMDAyMjIgMDAwMDAgbg0KMDAwMDAwMDM5MCAwMDAwMCBuDQowMDAwMDAxNTIyIDAwMDAwIG4NCjAwMDAwMDE2OTAgMDAwMDAgbg0KMDAwMDAwMjQyMyAwMDAwMCBuDQowMDAwMDAyNDU2IDAwMDAwIG4NCjAwMDAwMDI1NzQgMDAwMDAgbg0KDQp0cmFpbGVyDQo8PA0KL1NpemUgMTENCi9Sb290IDEgMCBSDQovSW5mbyAxMCAwIFINCj4+DQoNCnN0YXJ0eHJlZg0KMjcxNA0KJSVFT0YNCg=="),
                Format = "pdf",
                FileName = "teste"
            };
            var httpClient = CreateHttpClient(@"{""requestId"": ""4d6888d1-5ab5-4cf5-9d19-d43f16fd01d8"", ""status"": ""Done""}", "application/json");
            var ServiceClient = new ServiceClient(options, httpClient, ServiceClientLogger.Object);
            var result = await ServiceClient.CallAsync<SyncuploadingAFileForPositiveSelectionResult>(param, CancellationToken);
            var expectedResult = CreateExpectedResultApplicationJson(JsonConvert.DeserializeObject<SyncuploadingAFileForPositiveSelectionResult>(@"{""requestId"": ""4d6888d1-5ab5-4cf5-9d19-d43f16fd01d8"", ""status"": ""Done""}"));
            result.Result.Should().BeEquivalentTo(expectedResult.Result);
        }


        [Trait("Unit Test", "Success")]
        [Fact(DisplayName = "Should CallAsync Download")]
        public async Task ShouldCallAsync_Download()
        {
            using var stream = new MemoryStream();
            var file = Convert.FromBase64String("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");
            await stream.WriteAsync(file);
            var param = new HttpConfig
            {
                HttpMethod = HttpMethod.Get,
                RequestMultipartType = RequestMultipartType.Download,
                Endpoint = $"download/c842bf96-d517-43ec-9197-7d321c48b2bf"
            };
            var httpClient = CreateHttpClient<Stream>(stream,"application/octet-stream");
            var ServiceClient = new ServiceClient(options, httpClient, ServiceClientLogger.Object);
            var result = await ServiceClient.CallAsync<DownloadingAProcessedFileResult>(param, CancellationToken);
            var expectedResult = CreateExpectedResultFileContent(stream);
            Assert.NotNull(result);
        }

        #endregion

    }
}
