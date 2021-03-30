using Microsoft.EntityFrameworkCore;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.DataSources;
using NewsAggregator.Domain.Feeds;

namespace NewsAggregator.EF
{
    public class NewsAggregatorDBContext : DbContext
    {
        public NewsAggregatorDBContext(DbContextOptions<NewsAggregatorDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<FeedAggregate> Feeds { get; set; }
        public DbSet<ArticleAggregate> Articles { get; set; }
        public DbSet<DataSourceAggregate> DataSources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
