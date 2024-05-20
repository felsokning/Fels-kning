// ----------------------------------------------------------------------
// <copyright file="EntityFrameworkExtensions.cs" company="Felsökning">
//      Copyright © Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
// ----------------------------------------------------------------------
namespace Felsökning
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EntityFrameworkExtensions"/> class,
    ///     which is used to extend objects in the <see cref="Microsoft.EntityFrameworkCore"/> namespace.
    /// </summary>
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        ///     Extends the <see cref="DbSet{T}"/> class to surface a means to determine if an entry
        ///     exists in the Database Set, based on multiple property queries.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source that is contained within the <see cref="DbSet{T}"/>.</typeparam>
        /// <param name="dbSet">The <see cref="DbSet{T}"/> to perform the query against.</param>
        /// <param name="expressions">The <see cref="Func{T}"/> that is converted into the <see cref="Expression{T}"/></param>
        /// <returns>A <see cref="bool"/> indicating if the entity exists in the Database Set.</returns>
        public static bool EntityByPropertiesExists<T>(this DbSet<T> dbSet, Expression<Func<T, bool>>[] expressions)
            where T : class
        {
            // Exclude by using last predicate first, then progressing to the first.
            var reversedExpressions = expressions.Reverse().ToArray();
            var queryables = dbSet.AsQueryable();
            foreach(var reversedExpression in reversedExpressions)
            {
                queryables = queryables.Where(reversedExpression);
            }

            return queryables.Any();
        }
    }
}