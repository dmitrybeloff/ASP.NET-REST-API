using API.Domains.Interfaces;
using API.Services.Enum;
using API.Services.Mapper.Interaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace API.Services.Extensions
{
    /// <summary>
    /// A set of Linq extensions used to create reusable methods, which are intended to be used in Data Access services.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Maps a list if database entities to the list of corresponding model objects.
        /// </summary>
        /// <typeparam name="TSource">Type of a database entity.</typeparam>
        /// <typeparam name="TDest">Type of a model object type.</typeparam>
        /// <param name="query">Used to specify a list of objects to be mapped.</param>
        /// <param name="mapper">Used to specify a mapper object.</param>
        /// <returns>A list of the mapped objects</returns>
        public static List<TDest> MapEntitiesToList<TSource, TDest>(this IEnumerable<TSource> query, IMapper mapper) where TSource : class, IEntity where TDest : class, IDataTransferObject
        {
            List<TDest> result = new List<TDest>();
            foreach (var item in query)
            {
                result.Add(mapper.MapEntityToModel<TSource, TDest>(item));
            }
            return result;
        }

        /// <summary>
        /// Includes all the provided navigation properties.
        /// </summary>
        /// <typeparam name="TEntity">Type of a database entity.</typeparam>
        /// <param name="query">DbSet of <typeparamref name="TEntity"/>.</param>
        /// <param name="includeItems">A set of navigation properties.</param>
        /// <returns><paramref name="query"/> with the provided navigation properties included.</returns>
        public static IQueryable<TEntity> IncludeAll<TEntity>(this IQueryable<TEntity> query, IEnumerable<string> includeItems) where TEntity: class
        {
            foreach (var item in includeItems)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        /// <summary>
        /// Orders the <paramref name="query"/> using <paramref name="orderDelegate"/>.
        /// </summary>
        /// <typeparam name="TEntity">Type of a database entity.</typeparam>
        /// <typeparam name="TKey">Type of the object, that is used to order results.</typeparam>
        /// <param name="query">DbSet of <typeparamref name="TEntity"/>.</param>
        /// <param name="orderDelegate">Used to order results.</param>
        /// <param name="sortDirection">Used to specify the direction of the ordering.</param>
        /// <returns>Sorted <paramref name="query"/>.</returns>
        public static IQueryable<TEntity> ApplyOrdering<TEntity, TKey>(this IQueryable<TEntity> query, Expression<Func<TEntity, TKey>> orderDelegate, SortDirection sortDirection) where TEntity : class
        {
            if (orderDelegate != null)
            {
                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        query = query.OrderBy(orderDelegate);
                        break;
                    case SortDirection.Descending:
                        query = query.OrderByDescending(orderDelegate);
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}
