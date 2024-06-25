using Backend.AutoMapper;
using Backend.ServiceExtantions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Suggestions.Business.Abstract;
using Suggestions.Business.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Suggestions.DataAccess.Concrats;
using Suggestions.DataAccess.EfCore;
using NLog;


var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nLog.config"));
// Add services to the container.
builder.Services.AddControllersWithViews();




//var configuration = new ConfigurationBuilder()
//            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
//            .AddJsonFile("appsettings.json\", optional: false, reloadOnChange: true").Build();
builder.Services.ConfigureServices(builder.Configuration);

//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserService, UserManager>();
//builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

//builder.Services.AddDbContext<RepositoryContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
//});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
