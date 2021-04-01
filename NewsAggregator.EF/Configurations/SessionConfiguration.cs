using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Sessions;

namespace NewsAggregator.EF.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<SessionAggregate>
    {
        public void Configure(EntityTypeBuilder<SessionAggregate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvts);
            builder.HasMany(a => a.Actions).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
