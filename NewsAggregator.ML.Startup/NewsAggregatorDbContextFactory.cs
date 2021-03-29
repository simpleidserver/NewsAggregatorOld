using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NewsAggregator.EF;
using System.Reflection;

namespace NewsAggregator.ML.Startup
{
    public class NewsAggregatorDbContextFactory : IDesignTimeDbContextFactory<NewsAggregatorDBContext>
    {
        public NewsAggregatorDBContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<NewsAggregatorDBContext>();
            builder.UseSqlServer("Data Source=DESKTOP-T4INEAM\\SQLEXPRESS;Initial Catalog=NewsAggregator;Integrated Security=True",
                optionsBuilder => optionsBuilder.MigrationsAssembly(migrationsAssembly));
            return new NewsAggregatorDBContext(builder.Options);
        }
    }
}
