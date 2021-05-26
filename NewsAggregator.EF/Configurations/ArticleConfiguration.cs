using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.Articles;

namespace NewsAggregator.EF.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<ArticleAggregate>
    {
        public void Configure(EntityTypeBuilder<ArticleAggregate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Ignore(a => a.DomainEvts);
            builder.HasMany(a => a.ArticleLikeLst).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(a => a.ArticleReadLst).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
