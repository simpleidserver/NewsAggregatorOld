using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Recommendations;

namespace NewsAggregator.EF.Configurations
{
    public class RecommendationConfiguration : IEntityTypeConfiguration<RecommendationAggregate>
    {
        public void Configure(EntityTypeBuilder<RecommendationAggregate> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Ignore(_ => _.DomainEvts);
            builder.HasMany(_ => _.Articles).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
