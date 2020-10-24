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
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, bool>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<bool> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            IList<Book> books = new List<Book>();

            if (request.BookIds != null && request.BookIds.Any())
            {
                books = await bookRepository.FindWhereInAsync(request.BookIds);
            }

            var author = new Author(request.Name, books);

            await authorRepository.AddAsync(author);

            await authorRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
