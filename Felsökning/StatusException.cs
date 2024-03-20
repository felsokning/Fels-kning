// ----------------------------------------------------------------------
// <copyright file="StatusException.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StatusException"/> class.
    /// </summary>
    /// <inheritdoc cref="Exception"/>
#pragma warning disable S3925 // Superceded by SYSLIB0051 - See https://github.com/dotnet/docs/issues/34893
    [Serializable]
    public class StatusException : Exception
    {
#pragma warning restore S3925
        /// <summary>
        ///     Gets or sets the <see cref="Felsökning.HttpRecord"/> value associated with the failure.
        /// </summary>
        public HttpRecord HttpRecord { get; private set; } = new HttpRecord();

        /// <summary>
        ///     Gets or sets the Status Code for the <see cref="StatusException"/>.
        /// </summary>
        public string StatusCode { get; private set; } = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        public StatusException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="status">The status that describes the error.</param>
        public StatusException(string status)
            : base($"Invalid status given in response: {status}")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="statusCode">The status code that describes the error.</param>
        /// <param name="message">The message from the content body.</param>
        public StatusException(string statusCode, string message)
            : base($"Invalid status response received. Status: {statusCode}. Message: {message}")
        {
            StatusCode = statusCode;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="statusCode">The status code that describes the error.</param>
        /// <param name="message">The message from the content body.</param>
        /// <param name="httpRecord">The <see cref="Felsökning.HttpRecord"/> associated with the failure.</param>
        public StatusException(string statusCode, string message, HttpRecord httpRecord)
            : base($"Invalid status response received. Status: {statusCode}. Message: {message}")
        {
            HttpRecord = httpRecord;
            StatusCode = statusCode;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="message">The message from the content body.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public StatusException(string message, Exception innerException)
            : base($"Invalid status given in response: {message}", innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusException"/> class.
        ///     <inheritdoc cref="Exception"/>
        /// </summary>
        /// <param name="message">The message from the content body.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        /// <param name="httpRecord">The <see cref="Felsökning.HttpRecord"/> associated with the failure.</param>
        public StatusException(string message, Exception innerException, HttpRecord httpRecord)
            : base($"Invalid status given in response: {message}", innerException)
        {
            HttpRecord = httpRecord;
        }
    }
}