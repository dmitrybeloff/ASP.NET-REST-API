using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
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
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookModel>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public CreateBookCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<BookModel> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            IList<Author> authors = new List<Author>();

            if (request.AuthorIds != null && request.AuthorIds.Any())
            {
                authors = await authorRepository.FindWhereInAsync(request.AuthorIds);
            }

            var book = new Book(request.Title, authors);

            await bookRepository.AddAsync(book);

            await bookRepository.UnitOfWork.SaveChangesAsync();

            return mapper.MapEntityToModel<Book, BookModel>(book);
        }
    }
}
