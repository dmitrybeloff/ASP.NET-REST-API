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
    public class AddBookReviewCommandHandler : IRequestHandler<AddBookReviewCommand, BookReviewModel>
    {
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public AddBookReviewCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        public async Task<BookReviewModel> Handle(AddBookReviewCommand request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.FindAsync(request.BookId);

            if (book is null)
            {
                return default;
            }

            book.AddReview(request.Name, request.Stars, request.ReviewText);

            await bookRepository.UnitOfWork.SaveChangesAsync();

            return mapper.MapEntityToModel<BookReview, BookReviewModel>(book.BookReviews[0]);
        }
    }
}
