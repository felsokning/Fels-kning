namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="OasDateTimeJsonConverter" /> class,
    ///     which is used to add support for OpenAPI date-time format to Controllers in OpenAPI.
    /// </summary>
    /// <inheritdoc />
    public class OasDateTimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        ///     Reads and converts the JSON to type <see cref="DateTime"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString() ?? string.Empty, CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Writes a specified value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToOasString());
        }
    }
}
