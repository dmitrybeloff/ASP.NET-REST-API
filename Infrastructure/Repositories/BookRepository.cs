using API.Domains.Aggregates.BookAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        public IUnitOfWork UnitOfWork => databaseContext;

        public BookRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Book> AddAsync(Book book)
        {
            return (await databaseContext.Books.AddAsync(book)).Entity;
        }

        public async Task<Book> FindAsync(int id)
        {
            return await databaseContext.Books
                .SingleOrDefaultAsync(b => b.BookId.Value == id);
        }

        public async Task<IList<Book>> FindWhereInAsync(List<int> ids)
        {
            return await databaseContext.Books
                .Where(b => ids.Contains(b.BookId.Value))
                .ToListAsync();
        }

        public void Remove(Book book)
        {
            databaseContext.Remove(book);
        }
    }
}
