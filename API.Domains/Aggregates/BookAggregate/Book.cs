using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Events;
using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Domains.Aggregates.BookAggregate
{
    public class Book: Entity, IAggregateRoot
    {
        public int? BookId { get; private set; }
        public string Title { get; private set; }

        private readonly List<BookReview> bookReviews;
        public IReadOnlyList<BookReview> BookReviews => bookReviews;

        private readonly List<BookAuthorCatalog> bookAuthorCatalog;
        // The name Authors is used for compatibility with a read service mapper naming convention.
        public IReadOnlyList<BookAuthorCatalog> Authors => bookAuthorCatalog;

        protected Book() 
        {
            bookReviews = new List<BookReview>();
            bookAuthorCatalog = new List<BookAuthorCatalog>();
        }

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

        public void AddAuthor(Author author)
        {
            if (author != null)
            {
                AddDomainEvent(new BookAuthorConnectionCreatedDomainEvent(this, author));
            }            
        }

        public void Update(string title)
        {
            Title = title;
        }

        public void AddReview(string name, int stars, string reviewText)
        {
            var review = new BookReview(name, stars, reviewText, this);
            bookReviews.Add(review);
        }

        public void AddReview(BookReview bookReview)
        {
            bookReviews.Add(bookReview);
        }

        public bool UpdateReview(int reviewId, string name, int stars, string reviewText)
        {
            var review = bookReviews.SingleOrDefault(br => br.BookReviewId == reviewId);

            if (review != null)
            {
                review.UpdateReview(name, stars, reviewText);
                return true;
            }

            return false;
        }

        public bool DeleteReview(int reviewId)
        {
            var review = bookReviews.SingleOrDefault(br => br.BookReviewId == reviewId);

            if (review != null)
            {
                bookReviews.Remove(review);
                return true;
            }

            return false;
        }
    }
}
