using Microsoft.EntityFrameworkCore;
using NewsAggregator.Core.Domains.Articles;
using NewsAggregator.Core.Domains.DataSources;
using NewsAggregator.Core.Domains.Feeds;
using NewsAggregator.Core.Domains.Recommendations;
using NewsAggregator.Core.Domains.Sessions;

namespace NewsAggregator.EF
{
    public class NewsAggregatorDBContext : DbContext
    {
        public NewsAggregatorDBContext(DbContextOptions<NewsAggregatorDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<FeedAggregate> Feeds { get; set; }
        public DbSet<ArticleAggregate> Articles { get; set; }
        public DbSet<DataSourceAggregate> DataSources { get; set; }
        public DbSet<RecommendationAggregate> Recommendations { get; set; }
        public DbSet<SessionAggregate> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
