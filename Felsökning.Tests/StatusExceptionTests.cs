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
        [TestMethod]
        public void StatusException_ctor()
        {
            var sut = new StatusException();

            sut.Should().BeOfType<StatusException>();
        }

        [TestMethod]
        public void StatusException_Message()
        {
            var sut = new StatusException("On Fire");

            sut.Should().BeOfType<StatusException>();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status given in response: On Fire");
        }

        [TestMethod]
        public void StatusException_StatusCode_Message()
        {
            var sut = new StatusException("404", "On Fire");

            sut.Should().BeOfType<StatusException>();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status response received. Status: 404. Message: On Fire");
            var status = sut.StatusCode;
            status.Should().NotBeNullOrWhiteSpace();
            status.Should().Be("404");
        }

        [TestMethod]
        public void StatusException_WrappedException()
        {
            var baseException = new Exception("The Science is leaking out");

            var sut = new StatusException("On Fire", baseException);

            sut.Should().BeOfType<StatusException>();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status given in response: On Fire");
            var innerException = sut.InnerException;
            innerException.Should().BeOfType<Exception>();
            innerException?.Message.Should().Be("The Science is leaking out");
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

            sut.Should().BeOfType<StatusException>();
            var record = sut.HttpRecord;
            record.Should().BeOfType<HttpRecord>();
            var content = record.Content;
            content.Should().NotBeNullOrWhiteSpace();
            content.Should().Be("Stuff is on fire");
            var url = record.Url;
            url.Should().NotBeNullOrWhiteSpace();
            url.Should().Be("https://www.somefakedomain.co.gov.de.com");
            var statusCode = record.StatusCode;
            statusCode.Should().NotBeNullOrWhiteSpace();
            statusCode.Should().Be("404");
            var method = record.Method;
            method.Should().NotBeNullOrWhiteSpace();
            method.Should().Be("GET");
            var requestId = record.RequestId;
            requestId.Should().NotBeNullOrWhiteSpace();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status given in response: On Fire");
            var innerException = sut.InnerException;
            innerException.Should().BeOfType<Exception>();
            innerException?.Message.Should().Be("The Science is leaking out");
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

            sut.Should().BeOfType<StatusException>();
            var record = sut.HttpRecord;
            record.Should().BeOfType<HttpRecord>();
            var content = record.Content;
            content.Should().NotBeNullOrWhiteSpace();
            content.Should().Be("Stuff is on fire");
            var url = record.Url;
            url.Should().NotBeNullOrWhiteSpace();
            url.Should().Be("https://www.somefakedomain.co.gov.de.com");
            var statusCode = record.StatusCode;
            statusCode.Should().NotBeNullOrWhiteSpace();
            statusCode.Should().Be("404");
            var method = record.Method;
            method.Should().NotBeNullOrWhiteSpace();
            method.Should().Be("GET");
            var requestId = record.RequestId;
            requestId.Should().NotBeNullOrWhiteSpace();
            var message = sut.Message;
            message.Should().NotBeNullOrWhiteSpace();
            message.Should().Be("Invalid status response received. Status: 404. Message: On Fire");
            var status = sut.StatusCode;
            status.Should().NotBeNullOrWhiteSpace();
            status.Should().Be("404");
        }
    }
}
