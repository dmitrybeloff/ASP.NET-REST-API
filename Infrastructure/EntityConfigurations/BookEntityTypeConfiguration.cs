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
        }
    }
}
