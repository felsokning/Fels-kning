namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class OasDateTimeJsonConverterTests
    {
        private readonly OasDateTimeJsonConverter _sut = new();

        [TestMethod]
        public void OasDateTimeJsonConverter_Write_ShouldWriteDateTime()
        {
            // Arrange
            var dateTime = DateTime.UtcNow;
            var options = new JsonSerializerOptions();
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false });

            // Act
            _sut.Write(writer, dateTime, options);
            writer.Flush();
            var result = Encoding.UTF8.GetString(stream.ToArray());

            // Assert
            Assert.AreEqual($"\"{dateTime.ToOasString()}\"", result);
        }

        [TestMethod]
        public void OasDateTimeJsonConverter_Read_ShouldParseDateTime()
        {
            // Arrange
            var dateTime = new DateTime(2022, 9, 5, 19, 30, 50, DateTimeKind.Utc);
            var json = $"\"{dateTime.ToOasString()}\"";
            var bytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(bytes);
            var options = new JsonSerializerOptions();

            // Move to the string token
            reader.Read();

            // Act
            var result = _sut.Read(ref reader, typeof(DateTime), options);

            // Assert
            result.Should().Be(DateTime.Parse(dateTime.ToOasString(), CultureInfo.InvariantCulture));
        }

        [TestMethod]
        public void OasDateTimeJsonConverter_Read_InvalidFormat_ThrowsFormatException()
        {
            // Arrange
            var json = "\"not-a-date\"";
            var bytes = Encoding.UTF8.GetBytes(json);
            var reader = new Utf8JsonReader(bytes);
            var options = new JsonSerializerOptions();

            reader.Read();

            // Act & Assert - cannot capture ref local in lambda, use try/catch
            try
            {
                _sut.Read(ref reader, typeof(DateTime), options);
                Assert.Fail("Expected FormatException");
            }
            catch (FormatException)
            {
                // expected
            }
        }
    }
}
