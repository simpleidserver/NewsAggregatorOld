using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Recommendations;

namespace NewsAggregator.EF.Configurations
{
    public class RecommendationArticleConfiguration : IEntityTypeConfiguration<RecommendationArticle>
    {
        public void Configure(EntityTypeBuilder<RecommendationArticle> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}
