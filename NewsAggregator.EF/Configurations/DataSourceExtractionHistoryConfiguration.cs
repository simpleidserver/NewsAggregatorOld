using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.DataSources;

namespace NewsAggregator.EF.Configurations
{
    public class DataSourceExtractionHistoryConfiguration : IEntityTypeConfiguration<DataSourceExtractionHistory>
    {
        public void Configure(EntityTypeBuilder<DataSourceExtractionHistory> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
