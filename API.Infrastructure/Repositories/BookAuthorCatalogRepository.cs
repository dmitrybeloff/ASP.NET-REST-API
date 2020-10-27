using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories
{
    public class BookAuthorCatalogRepository : IBookAuthorCatalogRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        public IUnitOfWork UnitOfWork => databaseContext;

        public BookAuthorCatalogRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }        

        public async Task<BookAuthorCatalog> AddAsync(BookAuthorCatalog catalog)
        {
            return (await databaseContext.BookAuthorCatalog.AddAsync(catalog)).Entity;
        }

        public async Task<bool> AnyAsync(int bookId, int authorId)
        {
            return await databaseContext.BookAuthorCatalog
                .AnyAsync(ba => ba.Book.BookId == bookId && ba.Author.AuthorId == authorId);
        }

        public async Task<BookAuthorCatalog> FindByIdsAsync(int bookId, int authorId)
        {
            return await databaseContext.BookAuthorCatalog
                .SingleOrDefaultAsync(ba => ba.Book.BookId == bookId && ba.Author.AuthorId == authorId);
        }

        public void Remove(BookAuthorCatalog catalog)
        {
            databaseContext.Remove(catalog);
        }
    }
}
