using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure.Database;
using MediatR;
using API.Domains.Aggregates.BookAggregate;
using API.Infrastructure.Repositories;
using API.Domains.Aggregates.AuthorAggregate;
using API.DomainServices.Commands;

namespace API.ServicesExtensions
{
    public static partial class ServicesExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            //services.AddScoped<ICRUDService, CRUDService>();
            //services.AddScoped<IMapper, Mapper>();

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();

            services.AddMediatR(typeof(CreateAuthorCommandHandler));

            services.AddSwaggerGen();
        }
    }
}
