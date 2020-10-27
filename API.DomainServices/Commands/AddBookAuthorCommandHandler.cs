using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Services.Mapper.Interaces;
using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.DomainServices.Commands
{
    public class AddBookAuthorCommandHandler : IRequestHandler<AddBookAuthorCommand, bool>
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookAuthorCatalogRepository bookAuthorCatalogRepository;

        public AddBookAuthorCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, IBookAuthorCatalogRepository bookAuthorCatalogRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.bookAuthorCatalogRepository = bookAuthorCatalogRepository;
        }

        public async Task<bool> Handle(AddBookAuthorCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.FindAsync(request.BookId);

            var author = await authorRepository.FindAsync(request.AuthorId);

            if (book is null || author is null)
            {
                return false;
            }

            // This check is needed to preserve idempotency.
            if (await bookAuthorCatalogRepository.AnyAsync(request.BookId, request.AuthorId))
            {
                return true;
            }
            else
            {
                book.AddAuthor(author);

                await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }            
        }
    }
}
