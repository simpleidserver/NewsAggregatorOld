using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains;
using NewsAggregator.Core.Domains.Sessions;
using NewsAggregator.Core.Domains.Sessions.Enums;

namespace NewsAggregator.EF.Configurations
{
    public class SessionActionConfiguration : IEntityTypeConfiguration<SessionAction>
    {
        public void Configure(EntityTypeBuilder<SessionAction> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
            builder.Property(s => s.InteractionType).HasConversion(v => v.Id, v => Enumeration.FromId<InteractionTypes>(v));
        }
    }
}
