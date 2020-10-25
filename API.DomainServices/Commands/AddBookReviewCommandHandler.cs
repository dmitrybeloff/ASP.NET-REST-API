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
    public class AddBookReviewCommandHandler : IRequestHandler<AddBookReviewCommand, bool>
    {
        private readonly IBookRepository bookRepository;

        public AddBookReviewCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<bool> Handle(AddBookReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.FindAsync(request.BookId);

            if (book is null)
            {
                return false;
            }

            book.AddReview(request.Name, request.Stars, request.ReviewText);

            await bookRepository.UnitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
