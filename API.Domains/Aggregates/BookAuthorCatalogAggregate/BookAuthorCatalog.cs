using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates.BookAuthorCatalogAggregate
{
    public class BookAuthorCatalog: IEntity, IAggregateRoot, IManyToManyRelationshipMember
    {
        public int? BookAuthorCatalogId { get; private set; }

        public Book Book { get; private set; }
        public Author Author { get; private set; }

        protected BookAuthorCatalog() { }

        public BookAuthorCatalog(Book book, Author author)
        {
            Book = book;
            Author = author;
        }
    }
}
