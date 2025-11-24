namespace Felsökning.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class JsonSerializerOptionsBaseTests
    {
        [TestMethod]
        public void JsonSerializerOptions_ShouldHaveDefaultIgnoreCondition_WhenWritingNull()
        {
            // Arrange
            var optionsBase = new JsonSerializerOptionsBase();

            // Act
            var options = optionsBase.JsonSerializerOptions;

            // Assert
            Assert.IsNotNull(options);
            Assert.AreEqual(JsonIgnoreCondition.WhenWritingNull, options.DefaultIgnoreCondition);
        }
    }
}
