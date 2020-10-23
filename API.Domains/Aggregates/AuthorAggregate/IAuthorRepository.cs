using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domains.Aggregates.AuthorAggregate
{
    public interface IAuthorRepository: IRepository<Author>
    {
        Task<Author> Add(Author author);
        Author Remove(Author author);
        Task<Author> FindAsync(int id);
    }
}
