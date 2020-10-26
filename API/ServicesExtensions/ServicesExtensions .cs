using Microsoft.Extensions.DependencyInjection;
using MediatR;
using API.Domains.Aggregates.BookAggregate;
using API.Infrastructure.Repositories;
using API.Domains.Aggregates.AuthorAggregate;
using API.DomainServices.Commands;
using API.Domains.Aggregates.BookAuthorCatalogAggregate;
using API.Services.Mapper.Interaces;
using API.Services.Mapper;
using API.Services.Services.Interfaces;
using API.Services.Services;

namespace API.ServicesExtensions
{
    public static partial class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericReadService, GenericReadService>();
            services.AddScoped<IMapper, Mapper>();
            
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookAuthorCatalogRepository, BookAuthorCatalogRepository>();

            services.AddMediatR(typeof(CreateAuthorCommandHandler));

            services.AddSwaggerGen();
        }
    }
}
