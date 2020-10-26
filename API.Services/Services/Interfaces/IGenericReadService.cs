using API.Domains.Interfaces;
using API.Services.Enum;
using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Services.Interfaces
{
    /// <summary>
    /// Service that contains generic database read access operations.
    /// </summary>
    public interface IGenericReadService
    {
        /// <summary>
        /// Gets an entity value of type <typeparamref name="TSource"/> from the database using the <paramref name="predicate"/> predicate to filter result.
        /// Search result must be unique, otherwise the exception is thrown. Relative entities can be included.
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <typeparam name="TDest">View model type.</typeparam>
        /// <param name="predicate">Used to filter results.</param>
        /// <param name="include">Used to specify relative entities that should be included in result object.</param>
        /// <returns>An object of type <typeparamref name="TDest"/></returns>
        Task<TDest> ReadAsync<TSource, TDest>(Expression<Func<TSource, bool>> predicate, string[] include)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject;

        /// <summary>
        /// Gets a list of entity values of type <typeparamref name="TSource"/> from the database.
        /// Relative entities can be included.
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <typeparam name="TDest">View model type.</typeparam>
        /// <param name="start">Used to specify offset.</param>
        /// <param name="count">Used to specify count.</param>
        /// <param name="include">Used to specify relative entities that should be included in result object.</param>
        /// <returns>A list of objects of type <typeparamref name="TDest"/></returns>
        Task<List<TDest>> ReadManyAsync<TSource, TDest>(int start, int count, string[] include)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject;

        /// <summary>
        /// Gets a list of entity values of type <typeparamref name="TSource"/> from the database.
        /// Entities can be filtered. Relative entities can be included.
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <typeparam name="TDest">View model type.</typeparam>
        /// <param name="start">Used to specify offset.</param>
        /// <param name="count">Used to specify count.</param>
        /// <param name="include">Used to specify relative entities that should be included in result object.</param>
        /// <param name="predicate">Used to filter results.</param>
        /// <returns>A list of objects of type <typeparamref name="TDest"/></returns>
        Task<List<TDest>> ReadManyAsync<TSource, TDest>(int start, int count, string[] include, Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject;

        /// <summary>
        /// Gets a list of entity values of type <typeparamref name="TSource"/> from the database.
        /// Entities can be ordered. Relative entities can be included. Type <typeparamref name="TKey"/> is used in <paramref name="orderDelegate"/>.
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <typeparam name="TDest">View model type.</typeparam>
        /// <typeparam name="TKey">Type of the object, that is used to order results.</typeparam>
        /// <param name="start">Used to specify offset.</param>
        /// <param name="count">Used to specify count.</param>
        /// <param name="include">Used to specify relative entities that should be included in result object.</param>
        /// <param name="orderDelegate">Used to order results.</param>
        /// <param name="sortDirection">Used to specify the direction of the ordering.</param>
        /// <returns>A list of objects of type <typeparamref name="TDest"/></returns>
        Task<List<TDest>> ReadManyAsync<TSource, TDest, TKey>(int start, int count, string[] include, Expression<Func<TSource, TKey>> orderDelegate, SortDirection sortDirection = SortDirection.None)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject;

        /// <summary>
        /// Gets a list of entity values of type <typeparamref name="TSource"/> from the database.
        /// Entities can be filtered and ordered. Relative entities can be included. Type <typeparamref name="TKey"/> is used in <paramref name="orderDelegate"/>.
        /// </summary>
        /// <typeparam name="TSource">Entity type.</typeparam>
        /// <typeparam name="TDest">View model type.</typeparam>
        /// <typeparam name="TKey">Type of the object, that is used to order results.</typeparam>
        /// <param name="start">Used to specify offset.</param>
        /// <param name="count">Used to specify count.</param>
        /// <param name="include">Used to specify relative entities that should be included in result object.</param>
        /// <param name="predicate">Used to filter results.</param>
        /// <param name="orderDelegate">Used to order results.</param>
        /// <param name="sortDirection">Used to specify the direction of the ordering.</param>
        /// <returns>A list of objects of type <typeparamref name="TDest"/></returns>
        Task<List<TDest>> ReadManyAsync<TSource, TDest, TKey>(int start, int count, string[] include, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> orderDelegate, SortDirection sortDirection = SortDirection.None)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject;
    }
}
