using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.DomainServices.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, bool>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;

        public CreateBookCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<bool> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            IList<Author> authors = new List<Author>();

            if (request.AuthorIds != null && request.AuthorIds.Any())
            {
                authors = await authorRepository.FindWhereInAsync(request.AuthorIds);
            }

            var book = new Book(request.Title, authors);

            await bookRepository.AddAsync(book);

            await bookRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
