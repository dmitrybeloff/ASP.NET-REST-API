using API.Domains.Aggregates.BookAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Events;
using API.Domains.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates.AuthorAggregate
{
    public class Author: Entity, IAggregateRoot
    {
        public int? AuthorId { get; private set; }
        public string Name { get; private set; }

        private readonly List<BookAuthorCatalog> bookAuthorCatalog;
        // The name Books is used for compatibility with a read service mapper naming convention.
        public IReadOnlyList<BookAuthorCatalog> Books => bookAuthorCatalog;

        protected Author() 
        {
            bookAuthorCatalog = new List<BookAuthorCatalog>();
        }

        public Author(string name, IList<Book> books)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Author should have a name.");
            }

            Name = name;

            if (books != null)
            {
                foreach (var book in books)
                {
                    AddDomainEvent(new BookAuthorConnectionCreatedDomainEvent(book, this));
                }
            }
        }

        public void AddBook(Book book)
        {
            if (book != null)
            {
                AddDomainEvent(new BookAuthorConnectionCreatedDomainEvent(book, this));
            }            
        }
    }
}
