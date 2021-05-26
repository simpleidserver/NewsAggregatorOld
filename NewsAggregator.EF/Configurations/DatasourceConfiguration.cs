using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.DataSources;

namespace NewsAggregator.EF.Configurations
{
    public class DatasourceConfiguration : IEntityTypeConfiguration<DataSourceAggregate>
    {
        public void Configure(EntityTypeBuilder<DataSourceAggregate> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasMany(r => r.ExtractionHistories).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.Articles).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.Topics).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(a => a.DomainEvts);
        }
    }
}
