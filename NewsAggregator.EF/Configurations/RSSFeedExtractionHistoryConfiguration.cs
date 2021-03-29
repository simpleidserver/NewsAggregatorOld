using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Domain.RSSFeeds;

namespace NewsAggregator.EF.Configurations
{
    public class RSSFeedExtractionHistoryConfiguration : IEntityTypeConfiguration<RSSFeedExtractionHistory>
    {
        public void Configure(EntityTypeBuilder<RSSFeedExtractionHistory> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
