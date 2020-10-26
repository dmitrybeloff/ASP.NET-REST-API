using API.Domains.Interfaces;
using API.Infrastructure.Database;
using API.Services.Enum;
using API.Services.Extensions;
using API.Services.Mapper.Interaces;
using API.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Services.Services
{
    /// <inheritdoc/>
    public class GenericReadService : IGenericReadService
    {
        private readonly ApplicationDatabaseContext dbContext;
        private readonly IMapper mapper;

        /// <summary>
        /// Generic read service constructor. Uses ASP.NET's dependecy injecton mechanism to obtain <paramref name="dbContext"/> and <paramref name="mapper"/> objects.
        /// </summary>
        /// <param name="dbContext">Database context object.</param>
        /// <param name="mapper">Mapper object.</param>
        public GenericReadService(ApplicationDatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<TDest> ReadAsync<TSource, TDest>(Expression<Func<TSource, bool>> predicate, string[] include)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            IQueryable<TSource> query = dbContext.Set<TSource>();

            query = query.IncludeAll(include);

            // Single should be used here, not First. Even tho we are using predicate delegate to search, and the result value may not be unique, 
            // for example: multiple books with the same title can exist, this method should return value only if it is unique. 
            var result = await query.SingleAsync(predicate);

            var returnValue = mapper.MapEntityToModel<TSource, TDest>(result);

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task<List<TDest>> ReadManyAsync<TSource, TDest>(int start, int count, string[] include)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            IQueryable<TSource> query = dbContext.Set<TSource>();

            query = query.IncludeAll(include);

            var result = await query.Skip(start).Take(count).ToListAsync();

            return result.MapEntitiesToList<TSource, TDest>(mapper);
        }

        /// <inheritdoc/>
        public async Task<List<TDest>> ReadManyAsync<TSource, TDest>(int start, int count, string[] include, Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            IQueryable<TSource> query = dbContext.Set<TSource>();

            query = query.IncludeAll(include);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await query.Skip(start).Take(count).ToListAsync();

            return result.MapEntitiesToList<TSource, TDest>(mapper);
        }

        /// <inheritdoc/>
        public async Task<List<TDest>> ReadManyAsync<TSource, TDest, TKey>(int start, int count, string[] include, Expression<Func<TSource, TKey>> orderDelegate, SortDirection sortDirection)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            IQueryable<TSource> query = dbContext.Set<TSource>();

            query = query.IncludeAll(include);

            query = query.ApplyOrdering(orderDelegate, sortDirection);

            var result = await query.Skip(start).Take(count).ToListAsync();

            return result.MapEntitiesToList<TSource, TDest>(mapper);
        }

        /// <inheritdoc/>
        public async Task<List<TDest>> ReadManyAsync<TSource, TDest, TKey>(int start, int count, string[] include, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> orderDelegate, SortDirection sortDirection)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            IQueryable<TSource> query = dbContext.Set<TSource>();

            query = query.IncludeAll(include);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = query.ApplyOrdering(orderDelegate, sortDirection);

            var result = await query.Skip(start).Take(count).ToListAsync();

            return result.MapEntitiesToList<TSource, TDest>(mapper);
        }
    }
}
