using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Infrastructure.Database
{
    public class ApplicationDatabaseContext: DbContext, IUnitOfWork
    {
        public DbSet<Book> Books { get; set; }        
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthorCatalog> BookAuthorCatalog { get; set; }

        public ApplicationDatabaseContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorCatalogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookReviewEntityTypeConfiguration());
        }
    }    
}
