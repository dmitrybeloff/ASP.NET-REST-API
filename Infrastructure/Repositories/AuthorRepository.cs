using API.Domains.Aggregates.AuthorAggregate;
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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        public IUnitOfWork UnitOfWork => databaseContext;

        public AuthorRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Author> AddAsync(Author author)
        {
            return (await databaseContext.Authors.AddAsync(author)).Entity;
        }

        public async Task<Author> FindAsync(int id)
        {
            return await databaseContext.Authors
                .SingleOrDefaultAsync(a => a.AuthorId.Value == id);
        }

        public async Task<IList<Author>> FindWhereInAsync(List<int> ids)
        {
            return await databaseContext.Authors
                .Where(a => ids.Contains(a.AuthorId.Value))
                .ToListAsync();
        }

        public void Remove(Author author)
        {
            databaseContext.Remove(author);
        }
    }
}
