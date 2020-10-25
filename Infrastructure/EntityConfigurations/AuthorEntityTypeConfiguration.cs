using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.EntityConfigurations
{
    class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> authorConfiguration)
        {
            authorConfiguration.Ignore(e => e.DomainEvents);

            var booksNavigation = authorConfiguration.Metadata.FindNavigation(nameof(Author.Books));
            booksNavigation.SetField("bookAuthorCatalog");
            booksNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
