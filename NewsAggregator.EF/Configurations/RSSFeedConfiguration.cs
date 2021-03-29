using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.RSSFeeds;

namespace NewsAggregator.EF.Configurations
{
    public class RSSFeedConfiguration : IEntityTypeConfiguration<RSSFeedAggregate>
    {
        public void Configure(EntityTypeBuilder<RSSFeedAggregate> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasMany(r => r.ExtractionHistories).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(a => a.DomainEvts);
        }
    }
}
