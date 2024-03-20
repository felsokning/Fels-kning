// ----------------------------------------------------------------------
// <copyright file="AggregateExceptionExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AggregateExceptionExtensions"/> class.
    /// </summary>
    public static class AggregateExceptionExtensions
    {
        /// <summary>
        ///     Extends the <see cref="AggregateException"/> type to unbox/unwrap the exceptions contained within and return relevant data.
        ///     string[0] - hResults.
        ///     string[1] - Messages.
        ///     string[2] - StackTraces.
        /// </summary>
        /// <param name="aggregateException">The current <see cref="AggregateException"/> context.</param>
        /// <returns>A string array of relevant data.</returns>
        public static string[] Unbox(this AggregateException aggregateException)
        {
            using AggregateInternals internals = new();
            return internals.DelveInternally(exception: aggregateException);
        }
    }
}