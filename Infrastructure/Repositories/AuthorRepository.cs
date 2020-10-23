using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        public AuthorRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public IUnitOfWork UnitOfWork => databaseContext;

        public Task<Author> Add(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Author> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Author Remove(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
