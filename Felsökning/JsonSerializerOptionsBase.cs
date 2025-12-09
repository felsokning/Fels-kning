namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="JsonSerializerOptionsBase"/> class.
    /// </summary>
    public class JsonSerializerOptionsBase
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        /// <summary>
        ///     Gets the <see cref="JsonSerializerOptions"/> instance.
        /// </summary>
        public JsonSerializerOptions JsonSerializerOptions => jsonSerializerOptions;
    }
}
