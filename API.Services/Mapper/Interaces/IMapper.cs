using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Mapper.Interaces
{
    /// <summary>
    /// Mapper is used to bind values of Entity objects to Data Transfer Objects. Supports navigation properties binding.
    /// </summary>
    public interface IMapper
    {
        /// <summary>
        /// Recursively maps Entity object to Data Transfer Object.
        /// </summary>
        /// <typeparam name="TSource">Entity object type.</typeparam>
        /// <typeparam name="TDest">Data Transfer Object type.</typeparam>
        /// <param name="input">Entity object.</param>
        /// <param name="existingNames">Hashset containing names of the already mapped properties.</param>
        /// <returns>Data Transfer Object.</returns>
        TDest MapEntityToModel<TSource, TDest>(TSource input, HashSet<string> existingNames = null) where TSource : class, IEntity where TDest : class, IDataTransferObject;
    }
}
