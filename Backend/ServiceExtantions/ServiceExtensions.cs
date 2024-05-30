using Backend.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Suggestions.Business.Abstract;
using Suggestions.Business.Concrete;
using Suggestions.DataAccess.Concrats;
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
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Context'i yapılandır
            services.ConfigureSqlContext(configuration);

            // Diğer servisleri ekle
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped<IMailService, MailManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ISuggestionsService, SuggestionsManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddAutoMapper(typeof(MappingProfile));
        }

    }
}
