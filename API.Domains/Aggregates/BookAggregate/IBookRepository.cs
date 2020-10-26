using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Domains.Aggregates.BookAggregate
{
    public interface IBookRepository: IRepository<Book>
    {
        Task<Book> AddAsync(Book book);
        void Update(Book book);
        void Remove(Book book);
        Task<Book> FindAsync(int id);
        Task<Book> LoadBookWithReviewsAsync(int bookId);
        Task<IList<Book>> FindWhereInAsync(List<int> ids);
    }
}
