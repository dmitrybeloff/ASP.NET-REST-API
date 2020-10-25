using API.Domains.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.EntityConfigurations
{
    class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> bookConfiguration)
        {
            bookConfiguration.Ignore(e => e.DomainEvents);

            var authorsNavigation = bookConfiguration.Metadata.FindNavigation(nameof(Book.Authors));
            authorsNavigation.SetField("bookAuthorCatalog");
            authorsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            var reviewsNavigation = bookConfiguration.Metadata.FindNavigation(nameof(Book.BookReviews));
            reviewsNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
