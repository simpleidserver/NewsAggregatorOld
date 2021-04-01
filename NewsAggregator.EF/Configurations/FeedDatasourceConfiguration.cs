using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Feeds;

namespace NewsAggregator.EF.Configurations
{
    public class FeedDatasourceConfiguration : IEntityTypeConfiguration<FeedDatasource>
    {
        public void Configure(EntityTypeBuilder<FeedDatasource> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}
