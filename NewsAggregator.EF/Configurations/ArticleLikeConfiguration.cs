using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Articles;

namespace NewsAggregator.EF.Configurations
{
    public class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
    {
        public void Configure(EntityTypeBuilder<ArticleLike> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}
