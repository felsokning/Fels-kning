// ----------------------------------------------------------------------
// <copyright file="FileExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="FileExtensions"/> class.
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        ///     Asynchronously opens a text file, reads all the text in the file, then closes the file, then converts the text to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">The current instance of <see cref="IFile"/>.</param>
        /// <param name="filePath">The file to open for reading.</param>
        /// <returns>A task that represents the asynchronous read operation, which converts the string containing all text in the file to a <typeparamref name="T"/>.</returns>
        public static async Task<T> ReadAllTextAsync<T>(this IFile file, string filePath)
        {
            var textResult = await file.ReadAllTextAsync(filePath);
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            return JsonSerializer.Deserialize<T>(textResult, jsonSerializerOptions);
        }
    }
}