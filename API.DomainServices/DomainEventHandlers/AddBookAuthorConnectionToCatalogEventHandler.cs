using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.DomainServices.DomainEventHandlers
{
    class AddBookAuthorConnectionToCatalogEventHandler : INotificationHandler<BookAuthorConnectionCreatedDomainEvent>
    {
        private readonly IBookAuthorCatalogRepository bookAuthorCatalogRepository;

        public AddBookAuthorConnectionToCatalogEventHandler(IBookAuthorCatalogRepository bookAuthorCatalogRepository)
        {
            this.bookAuthorCatalogRepository = bookAuthorCatalogRepository;
        }

        public async Task Handle(BookAuthorConnectionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var bookAuthorCatalog = new BookAuthorCatalog(notification.Book, notification.Author);

            await bookAuthorCatalogRepository.AddAsync(bookAuthorCatalog);

            // bookAuthorCatalogRepository.UnitOfWork.SaveChangesAsync is not called here, because this notification handler can only be triggered
            // by SaveChangesAsync from somewhere else.
        }
    }
}
