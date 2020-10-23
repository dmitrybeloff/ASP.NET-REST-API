using API.Domains.Aggregates;
using API.Domains.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.EntityConfigurations
{
    class BookReviewEntityTypeConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> bookReviewConfiguration)
        {
            bookReviewConfiguration
                .HasOne(r => r.Book)
                .WithMany(b => b.BookReviews)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
