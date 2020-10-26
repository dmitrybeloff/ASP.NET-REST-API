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
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBookRepository bookRepository;

        public UpdateBookCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.FindAsync(request.BookId);

            if (book is null)
            {
                return default;
            }

            book.Update(request.Title);

            bookRepository.Update(book);

            await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
