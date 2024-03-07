// ----------------------------------------------------------------------
// <copyright file="IAsyncEnumerableExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="IAsyncEnumerableExtensions"/> class,
    ///     which is used to extend <see cref="IAsyncEnumerable{T}"/> types.
    /// </summary>
    public static class IAsyncEnumerableExtensions
    {
        /// <summary>
        ///     Asynchronously searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the data in the source.</typeparam>
        /// <param name="source">An asynchronous enumerable data source.</param>
        /// <param name="predicate">The <see cref="Predicate{T}"/> delegate that defines the conditions of the element to search for.</param>
        /// <returns>The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for <typeparamref name="TSource"/>.</returns>
        public static async Task<TSource?> FindAsync<TSource>(this IAsyncEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var enumerator = source.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                if (predicate(enumerator.Current)) return enumerator.Current;
            }

            return default;
        }

        /// <summary>
        ///     Executes a for-each operation on an <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the data in the source.</typeparam>
        /// <param name="source">An asynchronous enumerable data source.</param>
        /// <param name="action">The <see cref="Action{T}"/> delegate that is invoked once per element in the data source.</param>
        /// <returns>A <see cref="Task"/> that represents the entire for-each operation.</returns>
        public static async Task ForEachAsync<TSource>(this IAsyncEnumerable<TSource> source, Action<TSource> action)
        {
            var enumerator = source.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                try
                {
                    action(enumerator.Current);
                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        ///     Executes a for-each operation on an <see cref="IAsyncEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TSource">The type of the data in the source.</typeparam>
        /// <typeparam name="TResult">The type of the data in the output.</typeparam>
        /// <param name="source">An asynchronous enumerable data source.</param>
        /// <param name="action">The <see cref="Action{T}"/> delegate that is invoked once per element in the data source.</param>
        /// <returns>An <see cref="IAsyncEnumerable{T}"/> that represents the entire for-each operation.</returns>
        public static async IAsyncEnumerable<TResult> ForEachAsync<TSource, TResult>(this IAsyncEnumerable<TSource> source, Func<TSource, TResult> action)
        {
            var enumerator = source.GetAsyncEnumerator();
            while (await enumerator.MoveNextAsync())
            {
                yield return action(enumerator.Current);
            }
        }
    }
}