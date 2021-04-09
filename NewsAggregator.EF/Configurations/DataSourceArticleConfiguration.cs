using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsAggregator.Core.Domains.DataSources;

namespace NewsAggregator.EF.Configurations
{
    public class DataSourceArticleConfiguration : IEntityTypeConfiguration<DataSourceArticle>
    {
        public void Configure(EntityTypeBuilder<DataSourceArticle> builder)
        {
            builder.Property<int>("Id").ValueGeneratedOnAdd();
            builder.HasKey("Id");
        }
    }
}
