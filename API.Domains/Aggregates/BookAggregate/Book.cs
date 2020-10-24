using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Events;
using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates.BookAggregate
{
    public class Book: Entity, IAggregateRoot
    {
        public int? BookId { get; private set; }
        public string Title { get; set; }

        private readonly List<BookReview> bookReviews;
        public IReadOnlyList<BookReview> BookReviews => bookReviews;

        private readonly List<BookAuthorCatalog> bookAuthorCatalog;
        // The name Authors is used for compatibility with a read service mapper naming convention.
        public IReadOnlyList<BookAuthorCatalog> Authors => bookAuthorCatalog;

        protected Book() { }

        public Book(string title, IList<Author> authors)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException("Book should have a title.");
            }

            Title = title;

            bookReviews = new List<BookReview>();

            if (authors != null)
            {
                foreach (var author in authors)
                {
                    AddDomainEvent(new BookAuthorConnectionCreatedDomainEvent(this, author));
                }
            }
        }

        public void AddReview()
        {

        }
    }
}
