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
    public class DeleteBookAuthorCommandHandler : IRequestHandler<DeleteBookAuthorCommand, bool>
    {
        private readonly IBookAuthorCatalogRepository bookAuthorCatalogRepository;

        public DeleteBookAuthorCommandHandler(IBookAuthorCatalogRepository bookAuthorCatalogRepository)
        {
            this.bookAuthorCatalogRepository = bookAuthorCatalogRepository;
        }

        public async Task<bool> Handle(DeleteBookAuthorCommand request, CancellationToken cancellationToken)
        {
            var bookAuthorCatalogRecord = await bookAuthorCatalogRepository.FindByIdsAsync(request.BookId, request.AuthorId);

            if (bookAuthorCatalogRecord is null)
            {
                return false;
            }

            bookAuthorCatalogRepository.Remove(bookAuthorCatalogRecord);

            await bookAuthorCatalogRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return true;   
        }
    }
}
