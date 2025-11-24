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
        private HttpClient _httpClient;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/todos/";

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
        public async Task PatchAsync_WithValidData_ReturnsSuccessfulResponse()
        {
            // Arrange
            var patchTarget = new SampleJson();

            // Act
            var content = await _httpClient
                .PatchAsync<SampleJson>($"{BaseUrl}2", patchTarget)
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
                    .PatchAsync<SampleJson>($"{BaseUrl}3", patchTarget)
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
                    .PatchAsync<SampleJson>($"{BaseUrl}1000", patchTarget)
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
                .PostAsync<SampleJson>($"{BaseUrl}1", httpContent)
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
                .PostAsync<SampleJson>($"{BaseUrl}1", httpContent, contentType)
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
                    .PostAsync<SampleJson>($"{BaseUrl}3", httpContent, contentType)
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
                .PostAsync<SampleJson, SampleJson>($"{BaseUrl}1", postTarget)
                .ConfigureAwait(false);

            // Assert
            VerifySuccessfulPostResponse(result);
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