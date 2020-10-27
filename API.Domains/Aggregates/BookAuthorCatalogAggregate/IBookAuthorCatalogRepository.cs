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
        Task<bool> AnyAsync(int bookId, int authorId);
        Task<BookAuthorCatalog> FindByIdsAsync(int bookId, int authorId);
        void Remove(BookAuthorCatalog catalog);
    }
}
