using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

#pragma warning disable SA1652 // Enable XML documentation output
namespace GroceryStore.Data
#pragma warning restore SA1652 // Enable XML documentation output
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GroceryStoreDbContext>
    {
        public GroceryStoreDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<GroceryStoreDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            // Stop client query evaluation
            builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            return new GroceryStoreDbContext(builder.Options);
        }
    }
}
