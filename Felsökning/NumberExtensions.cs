//-----------------------------------------------------------------------
// <copyright file="NumberExtensions.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NumberExtensions"/> class.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        ///     Extends the <see cref="long"/> class to include validation if the given <see cref="long"/> is a valid Swedish personnummer via the Luhn Algorithm.
        /// </summary>
        /// <param name="value">The current <see cref="long"/> value context.</param>
        /// <returns>A boolean indicating if the checksum values match.</returns>
        public static bool IsValidPersonNummer(this long value)
        {
            return value.ToString().IsValidSwedishPersonNummer();
        }

        /// <summary>
        ///     Extends the <see cref="int"/> class to include validation if the given <see cref="int"/> is a valid Swedish personnummer via the Luhn Algorithm.
        /// </summary>
        /// <param name="value">The current <see cref="int"/> value context.</param>
        /// <returns>A boolean indicating if the checksum values match.</returns>
        public static bool IsValidPersonNummer(this int value)
        {
            return value.ToString().IsValidSwedishPersonNummer();
        }

        /// <summary>
        ///     Extends the <see cref="short"/> class to include validation if the given <see cref="short"/> is a valid Swedish personnummer via the Luhn Algorithm.
        /// </summary>
        /// <param name="value">The current <see cref="short"/> value context.</param>
        /// <returns>A boolean indicating if the checksum values match.</returns>
        public static bool IsValidPersonNummer(this short value)
        {
            return value.ToString().IsValidSwedishPersonNummer();
        }

#if NET7_0_OR_GREATER
        /// <summary>
        ///     Extends the <see cref="Int128"/> class to include validation if the given <see cref="Int128"/> is a valid Swedish personnummer via the Luhn Algorithm.
        /// </summary>
        /// <param name="value">The current <see cref="Int128"/> value context.</param>
        /// <returns>A boolean indicating if the checksum values match.</returns>
        public static bool IsValidPersonNummer(this Int128 value)
        {
            return value.ToString().IsValidSwedishPersonNummer();
        }
#endif
    }
}
