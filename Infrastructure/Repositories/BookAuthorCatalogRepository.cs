using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.Database;
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
    }
}
