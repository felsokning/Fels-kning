// ----------------------------------------------------------------------
// <copyright file="AggregateExceptionExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AggregateExceptionExtensionsTests
    {
        [TestMethod]
        public async Task AggregateException_Unbox()
        {
            var innerMostException = new Exception();
            var innerException = new Exception("Bad science", innerMostException);
            var argumentException = new ArgumentException("Argument Exception Message", innerException);
            var nullReferenceException = new NullReferenceException("Null Reference Exception Message", argumentException);
            var aggregateException = new AggregateException(nullReferenceException);

            var sut = await Assert.ThrowsExceptionAsync<AggregateException>(async () => await Task.FromException(aggregateException));

            var result = sut.Unbox();
            result.Should().NotBeNullOrEmpty();
            result.Length.Should().Be(3);
            var hresults = result[0];
            hresults.Should().NotBeNullOrEmpty();
            hresults.Should().Be("-2146233088\r\n-2147467261\r\n-2147024809\r\n");
            var messages = result[1];
            messages.Should().NotBeNullOrEmpty();
            messages.Should().Be("Exception of type 'System.Exception' was thrown.\r\nBad science\r\nArgument Exception Message\r\nNull Reference Exception Message\r\nOne or more errors occurred. (Null Reference Exception Message)\r\n");
            var stackTrace = result[2];
            stackTrace.Should().NotBeNullOrEmpty();
            stackTrace.Should().Contain("\r\n--- End of stack trace from previous location where exception was thrown ---\r\n");
        }
    }
}