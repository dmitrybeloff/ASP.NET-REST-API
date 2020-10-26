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
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorModel>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, IBookRepository bookRepository, IMapper mapper)
        {
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<AuthorModel> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            IList<Book> books = new List<Book>();

            if (request.BookIds != null && request.BookIds.Any())
            {
                books = await bookRepository.FindWhereInAsync(request.BookIds);
            }

            var author = new Author(request.Name, books);

            await authorRepository.AddAsync(author);

            await authorRepository.UnitOfWork.SaveChangesAsync();

            return mapper.MapEntityToModel<Author, AuthorModel>(author);
        }
    }
}
