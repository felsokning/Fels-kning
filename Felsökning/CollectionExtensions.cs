// ----------------------------------------------------------------------
// <copyright file="CollectionExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CollectionExtensions"/> class.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Extends the <see cref="ICollection{T}"/> interface to return an <see cref="IAsyncEnumerable{T}"/>,
        ///     which the iteration can be awaited through.
        /// </summary>
        /// <param name="values">The current <see cref="ICollection{T}"/> context.</param>
        /// <returns>An awaitable <see cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> ToIAsyncEnumerable<T>(this ICollection<T> values)
        {
            foreach (var item in values)
            {
                await Task.Delay(1);
                yield return item;
            }
        }

        /// <summary>
        ///     Extends the <see cref="IEnumerable{T}"/> interface to return an <see cref="IAsyncEnumerable{T}"/>,
        ///     which the iteration can be awaited through.
        /// </summary>
        /// <param name="values">The current <see cref="ICollection{T}"/> context.</param>
        /// <returns>An awaitable <see cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> ToIAsyncEnumerable<T>(this IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                await Task.Delay(1);
                yield return item;
            }
        }

        /// <summary>
        ///     Extends the <see cref="IList{T}"/> interface to return an <see cref="IAsyncEnumerable{T}"/>,
        ///     which the iteration can be awaited through.
        /// </summary>
        /// <param name="values">The current <see cref="ICollection{T}"/> context.</param>
        /// <returns>An awaitable <see cref="IAsyncEnumerable{T}"/>.</returns>
        public static async IAsyncEnumerable<T> ToIAsyncEnumerable<T>(this IList<T> values)
        {
            foreach (var item in values)
            {
                await Task.Delay(1);
                yield return item;
            }
        }
    }
}