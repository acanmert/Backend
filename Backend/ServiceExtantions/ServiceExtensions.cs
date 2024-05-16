using Microsoft.EntityFrameworkCore;
using Suggestions.DataAccess.EfCore;

namespace Backend.ServiceExtantions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Veritabanı bağlantı dizesini al
            services.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }
    }
}
