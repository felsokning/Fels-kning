// ----------------------------------------------------------------------
// <copyright file="TypeHttpResponse.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="TypeHttpResponse{T}"/> class.
    /// </summary>
    public class TypeHttpResponse<T>
    {
        /// <summary>
        ///     Gets or sets the Headers received in the response.
        /// </summary>
        public IEnumerable<KeyValuePair<string,IEnumerable<string>>> Headers { get; set; } = new List<KeyValuePair<string,IEnumerable<string>>>();

        /// <summary>
        ///     Gets or sets the Result of the request.
        /// </summary>
        public T Result { get; set; } = default(T);
    }
}