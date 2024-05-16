using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Suggestions.DataAccess.EfCore;

namespace Backend.RepositoryContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>()
    .       UseSqlServer(configuration.GetConnectionString("sqlConnection"),
            prj => prj.MigrationsAssembly("Backend"));

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
