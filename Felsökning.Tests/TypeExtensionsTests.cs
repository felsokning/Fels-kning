// ----------------------------------------------------------------------
// <copyright file="TypeExtensionsTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public async Task TypeExtensions_Should_ReturnDesiredHttpContent()
        {
            var sut = new SampleJson
            {
                Completed = true,
                Title = "A Test Title",
                Id = 8675309,
                UserId = 444,
            };

            var result = sut.ToJsonHttpContent();

            result.Should().NotBeNull();
            result.Headers.ContentType.Should().BeEquivalentTo(new MediaTypeHeaderValue("application/json"));

            var contentString = await result.ReadAsStringAsync();

            contentString.Should().NotBeNullOrWhiteSpace();
            contentString.Should().Be("{\"userId\":444,\"id\":8675309,\"title\":\"A Test Title\",\"completed\":true}");
        }

        [TestMethod]
        public void ToJsonHttpContent_ShouldThrowForNullInput()
        {
            SampleJson sut = null;

            var result = Assert.Throws<ArgumentNullException>(() => sut.ToJsonHttpContent());

            result.Should().NotBeNull();
            result.Should().BeOfType<ArgumentNullException>();
            result.Message.Should().Contain("Value cannot be null. (Parameter 'value')");
        }

        [TestMethod]
        public void ToJsonHttpContent_ShouldPassBackHttpContent()
        {
            var sut = new StringContent("This is string content!");
            var result = sut.ToJsonHttpContent();
            result.Should().NotBeNull();

            // StringContent Inherits from HttpContent
            result.Should().BeOfType<StringContent>();
        }
    }
}