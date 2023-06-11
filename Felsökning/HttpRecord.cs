// ----------------------------------------------------------------------
// <copyright file="HttpRecord.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="HttpRecord"/> class.
    /// </summary>
    public class HttpRecord
    {
        /// <summary>
        ///     Gets or sets the <see cref="Content"/> value,
        ///     which may contain reason[s] for the failure.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the <see cref="Method"/> value,
        ///     which indicates which HTTP Method was used.
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the <see cref="RequestId"/> value,
        ///     which can be used to track the failure context.
        /// </summary>
        public string RequestId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the <see cref="StatusCode"/> value,
        ///     which indicates the status code returned from the server.
        /// </summary>
        public string StatusCode { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the <see cref="Url"/> value,
        ///     which indicates which URL was attempted when the failure occurred.
        /// </summary>
        public string Url { get; set; } = string.Empty;
    }
}