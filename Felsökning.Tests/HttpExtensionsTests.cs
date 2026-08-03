// ----------------------------------------------------------------------
// <copyright file="HttpExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class HttpExtensionsTests
    {
        private HttpClient _httpClient = new();
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";
        public Microsoft.VisualStudio.TestTools.UnitTesting.TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            _httpClient = new HttpClient(new TestingHttpMessageHandler());
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpClient?.Dispose();
        }

        [TestMethod]
        public async Task AddNewRequestId_ShouldAddRequestIdHeaderToHttpClient()
        {
            // Arrange
            var requestId = Guid.NewGuid().ToString();
            // Act
            _httpClient.AddNewRequestId(requestId);
            // Assert
            _httpClient.DefaultRequestHeaders.Should().ContainSingle(
                header => header.Key == "X-Request-ID" && header.Value.Contains(requestId),
                "The X-Request-ID header should be added with the correct value."
            );
        }

        [TestMethod]
        public async Task AddHeaders_ShouldAddEachKeyValuePairToHttpClient()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "Header1", "Value1" },
                { "Header2", "Value2" }
            };

            _httpClient.AddHeaders(dictionary);

            _httpClient.DefaultRequestHeaders.Should().ContainSingle(
                header => header.Key == "Header1" && header.Value.Contains("Value1"),
                "The Header1 should be added with the correct value."
            );
            _httpClient.DefaultRequestHeaders.Should().ContainSingle(
                header => header.Key == "Header2" && header.Value.Contains("Value2"),
                "The Header2 should be added with the correct value."
            );
        }

        [TestMethod]
        public async Task PatchAsync_WithValidData_ReturnsSuccessfulResponse()
        {
            // Arrange
            var patchTarget = new SampleJson();

            // Act
            var content = await _httpClient
                .PatchAsync<SampleJson>($"{BaseUrl}2", patchTarget, TestContext.CancellationToken)
                .ConfigureAwait(false);

            // Assert
            content.Should().NotBeNull();
            content.Title.Should().NotBeNullOrWhiteSpace();
        }

        [TestMethod]
        public async Task PatchAsync_WithInvalidResource_ThrowsStatusException()
        {
            // Arrange
            var patchTarget = new SampleJson();

            // Act & Assert
            var exception = await Assert.ThrowsExactlyAsync<StatusException>(
                async () => await _httpClient
                    .PatchAsync<SampleJson>($"{BaseUrl}3", patchTarget, TestContext.CancellationToken)
                    .ConfigureAwait(false)
            ).ConfigureAwait(false);

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PatchAsync_WithNonExistentResource_ThrowsStatusExceptionWithInnerException()
        {
            // Arrange
            var patchTarget = new SampleJson();

            // Act & Assert
            var exception = await Assert.ThrowsExactlyAsync<StatusException>(
                async () => await _httpClient
                    .PatchAsync<SampleJson>($"{BaseUrl}1000", patchTarget, TestContext.CancellationToken)
                    .ConfigureAwait(false)
            ).ConfigureAwait(false);

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            exception.InnerException.Should().BeOfType<HttpRequestException>()
                .Which.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PostAsync_WithHttpContent_ReturnsSuccessfulResponse()
        {
            // Arrange
            var postTarget = new SampleJson();
            var httpContent = new StringContent(JsonSerializer.Serialize(postTarget));

            // Act
            var result = await _httpClient
                .PostAsync<SampleJson>($"{BaseUrl}1", httpContent, TestContext.CancellationToken)
                .ConfigureAwait(false);

            // Assert
            VerifySuccessfulPostResponse(result);
        }

        [TestMethod]
        public async Task PostAsync_WithJsonString_ReturnsSuccessfulResponse()
        {
            // Arrange
            var postTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(postTarget);
            var contentType = "application/json";

            // Act
            var result = await _httpClient
                .PostAsync<SampleJson>($"{BaseUrl}1", httpContent, contentType, TestContext.CancellationToken)
                .ConfigureAwait(false);

            // Assert
            VerifySuccessfulPostResponse(result);
        }

        [TestMethod]
        public async Task PostAsync_WithJsonString_ThrowsStatusException()
        {
            // Arrange
            var postTarget = new SampleJson();
            var httpContent = JsonSerializer.Serialize(postTarget);
            var contentType = "application/json";

            // Act & Assert
            var exception = await Assert.ThrowsExactlyAsync<StatusException>(
                async () => await _httpClient
                    .PostAsync<SampleJson>($"{BaseUrl}3", httpContent, contentType, TestContext.CancellationToken)
                    .ConfigureAwait(false)
            ).ConfigureAwait(false);

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PostAsync_WithGenericTypes_ReturnsSuccessfulResponse()
        {
            // Arrange
            var postTarget = new SampleJson();

            // Act
            var result = await _httpClient
                .PostAsync<SampleJson, SampleJson>($"{BaseUrl}1", postTarget, TestContext.CancellationToken)
                .ConfigureAwait(false);

            // Assert
            VerifySuccessfulPostResponse(result);
        }

        [TestMethod]
        public async Task RemoveHeader_ShouldRemoveTheHeaders()
        {
            // Arrange
            var headersToAdd = new Dictionary<string, string>
            {
                { "Header1", "Value1" },
                { "Header2", "Value2" }
            };
            _httpClient.AddHeaders(headersToAdd);
            // Act
            _httpClient.RemoveHeader("Header1");
            // Assert
            _httpClient.DefaultRequestHeaders.Should().NotContain(
                header => header.Key == "Header1",
                "The Header1 should be removed."
            );
            _httpClient.DefaultRequestHeaders.Should().Contain(
                header => header.Key == "Header2",
                "The Header2 should not be removed."
            );
        }

        private static void VerifySuccessfulPostResponse(SampleJson result)
        {
            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }
    }
}