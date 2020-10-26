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
    public class UpdateBookReviewCommandHandler : IRequestHandler<UpdateBookReviewCommand, bool>
    {
        private readonly IBookRepository bookRepository;

        public UpdateBookReviewCommandHandler(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<bool> Handle(UpdateBookReviewCommand request, CancellationToken cancellationToken)
        {
            // This is one of the things I do not like about DDD. By DDD patterns only aggregate can modify its child entities, in this particular 
            // case it means that I have to load all the reviews from the database, find the review that is meant to be changed and make all
            // the needed manipulations. I can't access child entities methods directly. 
            // But what if I have 10000 reviews in the database? Of course it would be unacceptable to strictly follow the DDD patterns and load
            // all of them each time i want to modify or delete one. 
            // For now I will stick with the proper DDD way and update the review entity through its aggregate.

            var book = await bookRepository.LoadBookWithReviewsAsync(request.BookId);

            if (book is null)
            {
                return default;
            }

            if (book.UpdateReview(request.BookReviewId, request.Name, request.Stars, request.ReviewText))
            {
                await bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
