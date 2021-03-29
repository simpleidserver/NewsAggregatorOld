using Microsoft.EntityFrameworkCore;
using NewsAggregator.Domain.Articles;
using NewsAggregator.Domain.RSSFeeds;

namespace NewsAggregator.EF
{
    public class NewsAggregatorDBContext : DbContext
    {
        public NewsAggregatorDBContext(DbContextOptions<NewsAggregatorDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<RSSFeedAggregate> RSSFeeds { get; set; }
        public DbSet<ArticleAggregate> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
