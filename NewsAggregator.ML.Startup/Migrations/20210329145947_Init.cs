using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewsAggregator.ML.Startup.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSSFeeds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSSFeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RSSFeedExtractionHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastArticlePublicationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    NbExtractedArticle = table.Column<int>(type: "int", nullable: false),
                    RSSFeedAggregateId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RSSFeedExtractionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RSSFeedExtractionHistory_RSSFeeds_RSSFeedAggregateId",
                        column: x => x.RSSFeedAggregateId,
                        principalTable: "RSSFeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RSSFeedExtractionHistory_RSSFeedAggregateId",
                table: "RSSFeedExtractionHistory",
                column: "RSSFeedAggregateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "RSSFeedExtractionHistory");

            migrationBuilder.DropTable(
                name: "RSSFeeds");
        }
    }
}
