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

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in ascending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByChained<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderBy(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, false);
            }

            return result;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in ascending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByChained<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderBy(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, false);
            }

            return result;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in ascending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in ascending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByChained<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderBy(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, false);
            }

            return result;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in descending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByDescendingChained<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderByDescending(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, true);
            }

            return result;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in descending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByDescendingChained<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderByDescending(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, true);
            }

            return result;
        }

        /// <summary>
        ///     Sorts the elements of a sequence in descending order.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelectors">An array of functions to extract a key from an element.</param>
        /// <returns>An <see cref="IOrderedEnumerable{TElement}"/> whose elements are sorted in descending order according to multiple keys.</returns>
        public static IOrderedEnumerable<TSource> OrderByDescendingChained<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey>[] keySelectors)
        {
            IOrderedEnumerable<TSource> result = source.OrderByDescending(keySelectors[0]);
            foreach (var keySelector in keySelectors)
            {
                // For some reason, if we don't do Func<TSource, TKey>[0], again, it is ignored.
                result = result.CreateOrderedEnumerable<TKey>(keySelector, null, true);
            }

            return result;
        }
    }
}