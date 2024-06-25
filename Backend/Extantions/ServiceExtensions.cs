using Backend.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Suggestions.Business.Abstract;
using Suggestions.Business.Concrete;
using Suggestions.DataAccess.Concrats;
using Suggestions.DataAccess.EfCore;
using System.Text;

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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = "MyAppCookie";
                    x.LoginPath = "/Account/LogIn/";
                });
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "http://localhost:5000",  // Token'ı oluşturan sunucu
            //        ValidAudience = "http://localhost:5000",  // Token'ı kullanacak olan uygulama
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_1234567890_1234567890")) // Aynı anahtarı kullanın
            //    };
            //});
            services.AddSingleton<ILoggerService, LoggerManager>();
        }

    }
}
