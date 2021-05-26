using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.DataSources;

namespace NewsAggregator.EF.Configurations
{
    public class DataSourceTopicConfiguration : IEntityTypeConfiguration<DataSourceTopic>
    {
        public void Configure(EntityTypeBuilder<DataSourceTopic> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
        }
    }
}
