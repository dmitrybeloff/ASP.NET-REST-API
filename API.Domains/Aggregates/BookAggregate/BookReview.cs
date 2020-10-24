using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates.BookAggregate
{
    public class BookReview: Entity
    {
        public int? BookReviewId { get; private set; }
        public string Name { get; private set; }
        public int Stars { get; private set; }
        public string ReviewText { get; private set; }
        public Book Book { get; private set; }

        protected BookReview() { }

        internal BookReview(string name, int stars, string reviewText, Book book)
        {
            if (string.IsNullOrEmpty(name))
            {
                Name = "Unknown User";
            }
            else
            {
                Name = name;
            }

            Stars = stars switch
            {
                var s when s < 1 => 1,
                var s when s > 5 => 5,
                _ => stars,
            };

            ReviewText = reviewText ?? string.Empty;

            Book = book;
        }
    }
}
