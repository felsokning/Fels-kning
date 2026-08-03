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
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = new List<T>();
                // Read the start array token
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        break;
                    }

                    // Deserialize each element as T
                    var item = JsonSerializer.Deserialize<T>(ref reader, options);
                    list.Add(item!);
                }

                return list;
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                // Parse the object and look for an "Items" property
                using JsonDocument doc = JsonDocument.ParseValue(ref reader);
                if (doc.RootElement.TryGetProperty("Items", out JsonElement itemsElement) && itemsElement.ValueKind == JsonValueKind.Array)
                {
                    var list = new List<T>();
                    foreach (var el in itemsElement.EnumerateArray())
                    {
                        var item = JsonSerializer.Deserialize<T>(el.GetRawText(), options);
                        list.Add(item!);
                    }

                    return list;
                }

                return null;
            }

            if (reader.TokenType == JsonTokenType.Number || reader.TokenType == JsonTokenType.String || reader.TokenType == JsonTokenType.True || reader.TokenType == JsonTokenType.False)
            {
                var single = JsonSerializer.Deserialize<T>(ref reader, options);
                return new List<T>() { single! };
            }

            throw new JsonException();
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            // Write as a JSON array to avoid re-entering this converter
            writer.WriteStartArray();
            foreach (var item in value)
            {
                JsonSerializer.Serialize(writer, item, options);
            }

            writer.WriteEndArray();
        }


        private sealed record Wrapper(List<T> Items);
    }
}