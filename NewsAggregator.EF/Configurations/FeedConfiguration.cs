using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Feeds;

namespace NewsAggregator.EF.Configurations
{
    public class FeedConfiguration : IEntityTypeConfiguration<FeedAggregate>
    {
        public void Configure(EntityTypeBuilder<FeedAggregate> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasMany(f => f.DataSources).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(f => f.DomainEvts);
        }
    }
}
