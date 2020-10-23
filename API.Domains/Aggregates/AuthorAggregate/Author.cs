using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Domains.Aggregates.AuthorAggregate
{
    public class Author: IEntity, IAggregateRoot
    {
        public int? AuthorId { get; private set; }
        public string Name { get; private set; }

        private readonly List<BookAuthorCatalog> bookAuthorCatalog;
        // The name Books is used for compatibility with a read service mapper naming convention.
        public IReadOnlyList<BookAuthorCatalog> Books => bookAuthorCatalog;

        protected Author() { }

        public Author(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Author should have a name.");
            }

            Name = name;
        }
    }
}
