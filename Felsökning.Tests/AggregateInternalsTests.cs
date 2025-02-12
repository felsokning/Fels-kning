// ----------------------------------------------------------------------
// <copyright file="AggregateInternalsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AggregateInternalsTests
    {
        [TestMethod]
        public void AggregateInternals_Constructor_InitializesCorrectly()
        {
            using var sut = new AggregateInternals();

            sut.Should().NotBeNull();
            sut.GetType().GetProperty("HResults", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("Messages", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("StackTraces", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("ReturnStrings", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<string[]>().Which.Should().BeEmpty();
        }

        [TestMethod]
        public void AggregateInternals_DelveInternally_ProcessesExceptionCorrectly()
        {
            using var sut = new AggregateInternals();
            var exception = new AggregateException("Test exception", new AggregateException("Inner Aggregate Exception", new AggregateException("Further Inner Aggregate Exception", new Exception("Inner exception"))));

            var result = sut.DelveInternally(exception);

            result.Should().NotBeNull();
            result.Length.Should().Be(3);
            result[0].Should().Contain("0");
            result[1].Should().Contain("Test exception").And.Contain("Inner exception");
            result[2].Should().NotBeNull();
        }

        [TestMethod]
        public void AggregateInternals_Dispose_ClearsListsAndArray()
        {
            using var sut = new AggregateInternals();
            var exception = new AggregateException("Test exception", new Exception("Inner exception"));

            sut.DelveInternally(exception);
            sut.Dispose();

            sut.GetType().GetProperty("HResults", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("Messages", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("StackTraces", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<List<string>>().Which.Should().BeEmpty();
            sut.GetType().GetProperty("ReturnStrings", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sut).Should().BeOfType<string[]>().Which.Should().BeEmpty();
        }
    }
}
