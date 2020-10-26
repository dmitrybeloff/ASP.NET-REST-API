using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services.Mapper.Interaces
{
    public interface IMapper
    {
        TDest MapEntityToModel<TSource, TDest>(TSource input, HashSet<string> existingNames = null) where TSource : class, IEntity where TDest : class, IDataTransferObject;
    }
}
