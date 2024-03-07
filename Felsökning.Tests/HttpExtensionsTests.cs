// ----------------------------------------------------------------------
// <copyright file="HttpExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HttpExtensionsTests
    {
        [TestMethod]
        public void ValidateHeaderAdded()
        {
            HttpClient client = new();
            client.AddHeader("Test", "testing");
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("Test"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("Test").FirstOrDefault()));
            Assert.IsTrue(client.DefaultRequestHeaders.GetValues("Test")?.FirstOrDefault()?.Equals("testing"));
        }

        [TestMethod]
        public void ValidateRequestIdGenerated()
        {
            HttpClient client = new();
            client.GenerateNewRequestId();
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("X-Request-ID"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
            client.GenerateNewRequestId();
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("X-Request-ID"));
            Assert.IsFalse(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
        }

        [TestMethod]
        public void ValidateRequestIsAdded()
        {
            HttpClient client = new();
            client.AddNewRequestId("155e3c68-d468-41f8-aa00-ed74c4f4d8f2");
            Assert.IsTrue(client.DefaultRequestHeaders.Contains("X-Request-ID"));
            var requestId = client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault();
            requestId.Should().NotBeNullOrWhiteSpace();
            requestId.Should().Be("155e3c68-d468-41f8-aa00-ed74c4f4d8f2");           
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ValidateHeaderRemoved()
        {
            HttpClient client = new();
            client.GenerateNewRequestId();
            client.RemoveHeader("X-Request-ID");
            Assert.IsTrue(string.IsNullOrWhiteSpace(client.DefaultRequestHeaders.GetValues("X-Request-ID").FirstOrDefault()));
        }

        [TestMethod]
        public void ValidateNonExistingHeaderRemoved()
        {
            HttpClient client = new();
            client.RemoveHeader("X-Request-ID");
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_Succeeds()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson content = await client.GetAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1");
            Assert.IsNotNull(content);
            Assert.IsNotNull(content.Title);
            Assert.IsFalse(string.IsNullOrWhiteSpace(content.Title));
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.GetAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3"));
            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: NotFound. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task GetDeserializedTypeData_ThrowsStatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.GetAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000"));
            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PatchData_Succeeds()
        {
            HttpClient httpClient = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            SampleJson content = await httpClient.PatchAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/2", patchTarget);
            Assert.IsNotNull(content);
            Assert.IsNotNull(content.Title);
            Assert.IsFalse(string.IsNullOrWhiteSpace(content.Title));
        }

        [TestMethod]
        public async Task PatchData_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", patchTarget));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PatchData_ThrowsStatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", patchTarget));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Succeeds()
        {
            HttpClient httpClient = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var result = await httpClient.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent);

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_HttpContent_Throws_StatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Succeeds()
        {
            HttpClient httpClient = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var result = await httpClient.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, contentType);

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, contentType));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PostDeserializedTypeData_String_Throws_StatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, contentType));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Succeeds()
        {
            HttpClient httpClient = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var result = await httpClient.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent);

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_HttpContent_Throws_StatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = new StringContent(JsonSerializer.Serialize(patchTarget));

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Succeeds()
        {
            HttpClient httpClient = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var result = await httpClient.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1", httpContent, contentType);

            result.Should().NotBeNull();
            result.Completed.Should().BeTrue();
            result.Id.Should().Be(8675309);
            result.Title.Should().Be("Super Secret and Diabolical Plans");
            result.UserId.Should().Be(24);
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Throws_StatusException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/3", httpContent, contentType));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status response received. Status: Received NotFound - Not Found from 'https://jsonplaceholder.typicode.com/todos/3'. Message: The resource didn't exist, yo.");
            exception.InnerException.Should().BeNull();
        }

        [TestMethod]
        public async Task PutDeserializedTypeData_String_Throws_StatusException_ForException()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";

            var exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/1000", httpContent, contentType));

            exception.Should().BeOfType<StatusException>();
            exception.Message.Should().Be("Invalid status given in response: NotFound - Resource Not Found from 'https://jsonplaceholder.typicode.com/todos/1000'");
            var innerException = exception.InnerException;
            innerException.Should().BeOfType<HttpRequestException>();
            innerException?.Message.Should().Be("Resource Not Found");
        }

        [TestMethod]
        public async Task NullResponseCode_Is_Handled()
        {
            HttpClient client = new(new TestingHttpMessageHandler());
            SampleJson patchTarget = new();
            var httpContent = JsonSerializer.Serialize(patchTarget);
            var contentType = "application/json";
            var stringContent = new StringContent(httpContent, new MediaTypeHeaderValue("application/json"));

            var getException = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.GetAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999"));
            var patchException = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PatchAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999", patchTarget));
            var postException = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999", httpContent, contentType));
            var post2Exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PostAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999", stringContent));
            var putException = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999", httpContent, contentType));
            var put2Exception = await Assert.ThrowsExceptionAsync<StatusException>(async () => await client.PutAsync<SampleJson>("https://jsonplaceholder.typicode.com/todos/999999", stringContent));

            getException.Should().BeOfType<StatusException>();
            patchException.Should().BeOfType<StatusException>();
            postException.Should().BeOfType<StatusException>();
            post2Exception.Should().BeOfType<StatusException>();
            putException.Should().BeOfType<StatusException>();
            put2Exception.Should().BeOfType<StatusException>();
        }
    }
}