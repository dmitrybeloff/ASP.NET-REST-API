using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Events
{
    public class BookAuthorConnectionCreatedDomainEvent: INotification
    {
        public Book Book { get; }
        public Author Author { get; }

        public BookAuthorConnectionCreatedDomainEvent(Book book, Author author)
        {
            Book = book;
            Author = author;
        }
    }
}
