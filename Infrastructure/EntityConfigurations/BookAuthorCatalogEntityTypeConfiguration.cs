using API.Domains.Aggregates;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.EntityConfigurations
{
    class BookAuthorCatalogEntityTypeConfiguration : IEntityTypeConfiguration<BookAuthorCatalog>
    {
        public void Configure(EntityTypeBuilder<BookAuthorCatalog> bookAuthorCatalogConfiguration)
        {
            bookAuthorCatalogConfiguration
                .HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .OnDelete(DeleteBehavior.Cascade);

            bookAuthorCatalogConfiguration
                .HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
