using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Articles;

namespace NewsAggregator.EF.Configurations
{
    public class ArticleReadConfiguration : IEntityTypeConfiguration<ArticleRead>
    {
        public void Configure(EntityTypeBuilder<ArticleRead> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}
