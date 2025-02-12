// ----------------------------------------------------------------------
// <copyright file="HttpJsonClientsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class HttpBaseTests
    {
        [TestMethod]
        public void HttpBaseConstructors()
        {
            // Version bumps with the executing worflow, so we have to fetch it.
            var versionString = Assembly.GetExecutingAssembly()!.GetName()!.Version!.ToString();

            var httpBaseVerion1 = new HttpBase(HttpVersion.Version11, "Science");
            var httpBaseVerion2 = new HttpBase(HttpVersion.Version20, "Science");
            var httpBaseVerion3 = new HttpBase(HttpVersion.Version30, "Science");

            httpBaseVerion1.Should().NotBeNull();
            httpBaseVerion1.Should().BeOfType<HttpBase>();
            httpBaseVerion2.Should().NotBeNull();
            httpBaseVerion2.Should().BeOfType<HttpBase>();
            httpBaseVerion3.Should().NotBeNull();
            httpBaseVerion3.Should().BeOfType<HttpBase>();

            httpBaseVerion1.HttpClient.Should().NotBeNull();
            httpBaseVerion2.HttpClient.Should().NotBeNull();
            httpBaseVerion3.HttpClient.Should().NotBeNull();

            httpBaseVerion1.HttpClient.DefaultRequestVersion.Should().Be(HttpVersion.Version11);
            httpBaseVerion1.HttpClient.DefaultVersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrHigher);
            httpBaseVerion2.HttpClient.DefaultRequestVersion.Should().Be(HttpVersion.Version20);
            httpBaseVerion2.HttpClient.DefaultVersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrHigher);
            httpBaseVerion3.HttpClient.DefaultRequestVersion.Should().Be(HttpVersion.Version30);
            httpBaseVerion3.HttpClient.DefaultVersionPolicy.Should().Be(HttpVersionPolicy.RequestVersionOrHigher);

            var http1UserAgents = httpBaseVerion1.HttpClient.DefaultRequestHeaders.UserAgent;
            var http2UserAgents = httpBaseVerion2.HttpClient.DefaultRequestHeaders.UserAgent;
            var http3UserAgents = httpBaseVerion3.HttpClient.DefaultRequestHeaders.UserAgent;

            http1UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Science"))!.Product!.Version.Should().Contain(versionString);
            http1UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Contact"))!.Product!.Version.Should().Contain("nuget%40felsokning.se");
            http1UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Website"))!.Product!.Version.Should().Contain("https%3A%2F%2Fwww.nuget.org%2Fprofiles%2Ffelsokning");
            http2UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Science"))!.Product!.Version.Should().Contain(versionString);
            http2UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Contact"))!.Product!.Version.Should().Contain("nuget%40felsokning.se");
            http2UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Website"))!.Product!.Version.Should().Contain("https%3A%2F%2Fwww.nuget.org%2Fprofiles%2Ffelsokning");
            http3UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Science"))!.Product!.Version.Should().Contain(versionString);
            http3UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Contact"))!.Product!.Version.Should().Contain("nuget%40felsokning.se");
            http3UserAgents.FirstOrDefault(x => x.Product!.Name.Contains("Website"))!.Product!.Version.Should().Contain("https%3A%2F%2Fwww.nuget.org%2Fprofiles%2Ffelsokning");

            // Check for SSL Negotiation errors.

            httpBaseVerion1.Dispose();
            httpBaseVerion2.Dispose();
            httpBaseVerion3.Dispose();
        }
    }
}