// ----------------------------------------------------------------------
// <copyright file="SingleItemOrListConverterTests.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>GitHub Copilot</author>
// ----------------------------------------------------------------------
namespace Felsökning.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SingleItemOrListConverterTests
    {
        [TestMethod]
        public void Read_Array_ReturnsList()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SingleItemOrListConverter<int>());

            var json = "[1,2,3]";

            var result = JsonSerializer.Deserialize<List<int>>(json, options);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Read_Number_ReturnsSingleItemList()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SingleItemOrListConverter<int>());

            var json = "42";

            var result = JsonSerializer.Deserialize<List<int>>(json, options);

            result.Should().NotBeNull();
            result.Should().ContainSingle().Which.Should().Be(42);
        }

        [TestMethod]
        public void Read_ObjectWithItems_ReturnsItems()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SingleItemOrListConverter<int>());

            var json = "{ \"Items\": [ 7, 8 ] }";

            var result = JsonSerializer.Deserialize<List<int>>(json, options);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new[] { 7, 8 });
        }

        [TestMethod]
        public void Read_InvalidToken_ThrowsJsonException()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SingleItemOrListConverter<int>());

            var json = "\"a string\"";

            Action act = () => JsonSerializer.Deserialize<List<int>>(json, options);

            act.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void Write_SerializesAsArray()
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new SingleItemOrListConverter<int>());

            var list = new List<int> { 9, 10 };

            var json = JsonSerializer.Serialize(list, options);

            json.Should().Be("[9,10]");
        }
    }
}
