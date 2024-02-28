namespace Felsökning.Tests
{
    [TestClass]
    public class HttpBaseTests
    {
        [TestMethod]
        public async Task TestTlsVersions()
        {
            var sut = new HttpBase(HttpVersion.Version11, "Felsökning.Tests");

            var exception = await Assert.ThrowsExceptionAsync<HttpRequestException>(async () => await sut.HttpClient.GetStringAsync("https://api.nationaltransport.ie/gtfsr/v2/TripUpdates?format=json"));
            exception.Should().BeOfType<HttpRequestException>();
        }
    }
}