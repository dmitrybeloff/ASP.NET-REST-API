using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domains.Aggregates.BookAuthorCatalogAggregate
{
    public interface IBookAuthorCatalogRepository : IRepository<BookAuthorCatalog>
    {
        Task<BookAuthorCatalog> AddAsync(BookAuthorCatalog catalog);
    }
}
