// ----------------------------------------------------------------------
// <copyright file="StatusExceptionTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StatusExceptionTests
    {
        private static void AssertStatusException(StatusException sut, string expectedMessage, string? expectedStatusCode = null, Exception? expectedInnerException = null, HttpRecord? expectedHttpRecord = null)
        {
            sut.Should().BeOfType<StatusException>();
            sut.Message.Should().NotBeNullOrWhiteSpace();
            sut.Message.Should().Be(expectedMessage);

            if (expectedStatusCode != null)
            {
                sut.StatusCode.Should().NotBeNullOrWhiteSpace();
                sut.StatusCode.Should().Be(expectedStatusCode);
            }

            if (expectedInnerException != null)
            {
                sut.InnerException.Should().BeOfType<Exception>();
                sut.InnerException?.Message.Should().Be(expectedInnerException.Message);
            }

            if (expectedHttpRecord != null)
            {
                sut.HttpRecord.Should().BeOfType<HttpRecord>();
                sut.HttpRecord.Content.Should().NotBeNullOrWhiteSpace();
                sut.HttpRecord.Content.Should().Be(expectedHttpRecord.Content);
                sut.HttpRecord.Url.Should().NotBeNullOrWhiteSpace();
                sut.HttpRecord.Url.Should().Be(expectedHttpRecord.Url);
                sut.HttpRecord.StatusCode.Should().NotBeNullOrWhiteSpace();
                sut.HttpRecord.StatusCode.Should().Be(expectedHttpRecord.StatusCode);
                sut.HttpRecord.Method.Should().NotBeNullOrWhiteSpace();
                sut.HttpRecord.Method.Should().Be(expectedHttpRecord.Method);
                sut.HttpRecord.RequestId.Should().NotBeNullOrWhiteSpace();
            }
        }

        [TestMethod]
        public void StatusException_ctor()
        {
            var sut = new StatusException();
            AssertStatusException(sut, "Exception of type 'Felsökning.StatusException' was thrown.");
        }

        [TestMethod]
        public void StatusException_Message()
        {
            var sut = new StatusException("On Fire");
            AssertStatusException(sut, "Invalid status given in response: On Fire");
        }

        [TestMethod]
        public void StatusException_StatusCode_Message()
        {
            var sut = new StatusException("404", "On Fire");
            AssertStatusException(sut, "Invalid status response received. Status: 404. Message: On Fire", "404");
        }

        [TestMethod]
        public void StatusException_WrappedException()
        {
            var baseException = new Exception("The Science is leaking out");
            var sut = new StatusException("On Fire", baseException);
            AssertStatusException(sut, "Invalid status given in response: On Fire", expectedInnerException: baseException);
        }

        [TestMethod]
        public void StatusException_WrappedException_HttpRecord()
        {
            var baseException = new Exception("The Science is leaking out");
            var httpRecord = new HttpRecord
            {
                Content = "Stuff is on fire",
                Url = "https://www.somefakedomain.co.gov.de.com",
                StatusCode = "404",
                Method = "GET",
                RequestId = Guid.NewGuid().ToString(),
            };
            var sut = new StatusException("On Fire", baseException, httpRecord);
            AssertStatusException(sut, "Invalid status given in response: On Fire", expectedInnerException: baseException, expectedHttpRecord: httpRecord);
        }

        [TestMethod]
        public void StatusException_StatusCode_Message_HttpRecord()
        {
            var httpRecord = new HttpRecord
            {
                Content = "Stuff is on fire",
                Url = "https://www.somefakedomain.co.gov.de.com",
                StatusCode = "404",
                Method = "GET",
                RequestId = Guid.NewGuid().ToString(),
            };
            var sut = new StatusException("404", "On Fire", httpRecord);
            AssertStatusException(sut, "Invalid status response received. Status: 404. Message: On Fire", "404", expectedHttpRecord: httpRecord);
        }
    }
}
