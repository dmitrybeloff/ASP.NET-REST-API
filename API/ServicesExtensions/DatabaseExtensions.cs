using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using API.Infrastructure.Database;

namespace API.ServicesExtensions
{
    public static partial class ServicesExtensions
    {
        public static readonly Microsoft.Extensions.Logging.LoggerFactory loggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDatabaseContext>(options =>
            {
                options.UseLoggerFactory(loggerFactory);
                options.UseSqlite(configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly("API"));
            }
            );
        }
    }
}
