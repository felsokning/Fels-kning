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
    }
}
