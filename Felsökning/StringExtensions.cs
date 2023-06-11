// ----------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StringExtensions"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Extends the <see cref="T:string[]"/> object to return a comma-separated string of the items in the string array.
        /// </summary>
        /// <param name="value">The current <see cref="T:string[]"/> object.</param>
        /// <returns>A string containing the string array's contents, comma-separated.</returns>
        public static string ToArrayString(this string[] value)
        {
            if (value == null || value.Length == 0)
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentNullException(nameof(value)));
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int c = 0; c <= value.Length - 1; c++)
            {
                if (c == value.Length - 1)
                {
                    stringBuilder.Append(value: value[c]);
                }
                else
                {
                    stringBuilder.Append(value: value[c] + ", ");
                }
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Validates the given string is not null, empty, or whitespace.
        /// </summary>
        /// <param name="value">The current string value context.</param>
        public static void Validate(this string value)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentException($"The given string was either null, empty, or whitespace", nameof(value)));
            }
        }

        /// <summary>
        ///     Validates the given string is not null, empty, or whitespace.
        ///     Also validates that the given string is a minimum length, as expected.
        /// </summary>
        /// <param name="value">The current string value context.</param>
        /// <param name="length">The expected length of the string.</param>
        public static void Validate(this string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value: value))
            {
                throw new StatusException(
                    "The given string was either null, empty, or whitespace",
                    new ArgumentException("The given string was either null, empty, or whitespace", nameof(value)));
            }

            if (!(value.Length == length))
            {
                throw new StatusException(
                    $"The given string was not the expected length of {length}",
                    new ArgumentException($"The given string was not the expected length of {length}", nameof(value)));
            }
        }
    }
}