using API.Domains.Aggregates;
using API.Domains.Aggregates.AuthorAggregate;
using API.Domains.Aggregates.BookAggregate;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Domains.Interfaces;
using API.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.Infrastructure.Database
{
    public class ApplicationDatabaseContext: DbContext, IUnitOfWork
    {
        private readonly IMediator mediator;

        public DbSet<Book> Books { get; set; }        
        public DbSet<BookReview> BookReviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthorCatalog> BookAuthorCatalog { get; set; }

        public ApplicationDatabaseContext(DbContextOptions options, IMediator mediator) : base(options) 
        {
            this.mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorCatalogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookReviewEntityTypeConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(e => e.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var publishTasks = domainEvents.Select(de => mediator.Publish(de));

            await Task.WhenAll(publishTasks);

            return await base.SaveChangesAsync();
        }
    }    
}
