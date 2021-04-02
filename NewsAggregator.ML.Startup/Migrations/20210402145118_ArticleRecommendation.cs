using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.ML.Startup.Migrations
{
    public partial class ArticleRecommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "RecommendationArticle",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "RecommendationArticle");
        }
    }
}
