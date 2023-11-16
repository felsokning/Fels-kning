//-----------------------------------------------------------------------
// <copyright file="SingleItemOrListConverter.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SingleItemOrListConverter{T}"/> class,
    ///     which is used to intercept returns when endpoints behave badly.
    /// </summary>
    /// <typeparam name="T">A referenced type.</typeparam>
    public class SingleItemOrListConverter<T> : JsonConverter<List<T>>
    {
        /// <inheritdoc/>
        public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)

            => reader.TokenType switch
            {
                JsonTokenType.StartArray =>
                    JsonSerializer.Deserialize<List<T>>(ref reader, options),
                JsonTokenType.StartObject =>
                    JsonSerializer.Deserialize<Wrapper>(ref reader, options)?.Items,
                JsonTokenType.Number =>
                    new List<T>()
                    {
                        JsonSerializer.Deserialize<T>(ref reader, options)!
                    },
                _ => throw new JsonException()
            };

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)

            => JsonSerializer.Serialize(writer, (object?)value, options);


        private sealed record Wrapper(List<T> Items);
    }
}