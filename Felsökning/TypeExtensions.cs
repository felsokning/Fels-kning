// ----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeExtensions"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Converts the given <typeparamref name="T"/> into a JSON string representation.
        /// </summary>
        /// <typeparam name="T">The current <typeparamref name="T"/> context.</typeparam>
        /// <param name="value">The current <typeparamref name="T"/> value.</param>
        /// <returns>An <see cref="HttpContent"/> object with the Content-Type header of "application/json".</returns>
        public static HttpContent ToJsonHttpContent<T>(this T value)
        {
            var typeString = JsonSerializer.Serialize(value);
            var stringHttpContent = new StringContent(typeString);
            stringHttpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringHttpContent;
        }
    }
}